using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ThirdPartyApiCaller.Utility;
using WeatherApp.Models;
using WeatherApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ThirdPartyApiCaller.Factory;
using ThirdPartyApiCaller.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;
        private readonly dbContext _context;

        public HomeController(IOptions<MySettingsModel> app, dbContext context)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.GeolocationUrl;
            _context = context;
        }

        public IActionResult Index()
        {
            int memberId = 1;

            WeeklyForecastModel forecastModel  = new WeeklyForecastModel();
            InitializeWeeklyForcast(forecastModel, memberId);
            return View(forecastModel);
        }
        
        [HttpPost]
        public IActionResult Index([FromForm] WeeklyForecastModel model)
        {
            int memberId = 1;
            string requestZip = model.SearchZip ?? model.SearchZipHistory;

            if (!String.IsNullOrWhiteSpace(requestZip))
            {
                AddSearchToHistory(requestZip, memberId);
            }

            WeeklyForecastModel forecastModel = new WeeklyForecastModel();
            InitializeWeeklyForcast(forecastModel, memberId);

            if (ModelState.IsValid)
            {
                ModelState.Clear();
            }

            forecastModel.SearchZip = requestZip;
            forecastModel.WeekForcast = GetWeeklyForecastForZipCode(forecastModel);
            forecastModel.SearchZip = null;
            
            return View(forecastModel);
        }

        private void AddSearchToHistory(string requestZip, int memberId)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var check = _context.UserHistory.Where(zip => zip.ZipCode.Equals(requestZip) && zip.MemberId == memberId);
                        //ensure data integrity.
                        if (check.Count<SearchHistory>() == 0)
                        {
                            SearchHistory item = new SearchHistory() { MemberId = memberId, ZipCode = requestZip };
                            _context.Add(item);

                            Task task = _context.SaveChangesAsync();
                            task.Wait();

                            transaction.Commit();
                        }

                    }

                    catch (DbUpdateConcurrencyException)
                    {
                        transaction.Rollback();
                        throw;
                    }


                }
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = "1"});
        }

        private async Task<List<SearchHistory>> GetUserSearchHistory(int memberId)
        {
            var results = await _context.UserHistory.Where(member => member.MemberId == memberId).ToListAsync();
            return results;
        }

        private void SetUserHistory(int memberId, WeeklyForecastModel model)
        {
            Task<List<SearchHistory>> t = GetUserSearchHistory(memberId);
            t.Wait();

            model.SearchHistory.Add("Select"); //default setting.

            foreach(SearchHistory item in t.Result)
            {
                model.SearchHistory.Add(item.ZipCode);
            }

            model.SearchHistoryItems = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(model.SearchHistory);
        }

        private void SetCurrentLocation(WeeklyForecastModel model)
        {
            Task<GeolocationModel> location = ApiClientFactory.Instance.CallGeolocationApi(appSettings.Value.GeolocationUrl);
            location.Wait();

            model.CurrentLocation = location.Result;
        }

        private void SetCurrentLocationForecast(WeeklyForecastModel model)
        {
            Forecast forecast = GetWeeklyForecastForZipCode(model);
            
            model.WeekForcast = forecast;            
        }

        private Forecast GetWeeklyForecastForZipCode(WeeklyForecastModel model)
        {
            string zip = model.SearchZip;

            appSettings.Value.WeatherZip = zip;

            model.DisplayedZip = zip;
            model.SearchZip = null;

            Task<Forecast> forecast = ApiClientFactory.Instance.CallWeatherApi(appSettings.Value.WeatherUrl);
            forecast.Wait();

            return forecast.Result;
        }

        private void InitializeWeeklyForcast(WeeklyForecastModel forecastModel, int memberId)
        {
            SetCurrentLocation(forecastModel);
            forecastModel.SearchZip = forecastModel.CurrentLocation.ZipCode;
            forecastModel.DisplayedZip = forecastModel.CurrentLocation.ZipCode;
            SetCurrentLocationForecast(forecastModel);
            AddSearchToHistory(forecastModel.DisplayedZip, memberId);
            SetUserHistory(memberId, forecastModel);

        }
    }
}
