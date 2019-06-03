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
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;
        private readonly dbContext _context;
        private readonly ILocationService _locationService;
        private readonly IUserService _userService;
        private readonly IWeatherForecastService _forecastService;

        #region Constructors
        public HomeController(IOptions<MySettingsModel> app, dbContext context, ILocationService location, 
            IUserService user, IWeatherForecastService forecast)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.GeolocationUrl;
            _locationService = location;
            _context = context;
            _userService = user;
            _forecastService = forecast;
        }
        #endregion Constructors

        #region Index
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

        #endregion Index

        #region About

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        #endregion About

        #region Contact
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        #endregion Contact

        #region Error
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = "1"});
        }
        #endregion Error

        #region View Model Manipulations
        private void SetUserHistory(int memberId, WeeklyForecastModel model)
        {
            List<SearchHistory> histories = _userService.GetUserSearchHistory(_context, memberId);

            model.SearchHistory.Add("Select"); //default setting.

            foreach(SearchHistory item in histories)
            {
                model.SearchHistory.Add(item.ZipCode);
            }

            model.SearchHistoryItems = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(model.SearchHistory);
        }

        private void AddSearchToHistory(string requestZip, int memberId)
        {
            if (ModelState.IsValid)
            {
                _userService.SaveZipCodeToSearchHistory(_context, memberId, requestZip);
            }
        }

        private void SetCurrentLocation(WeeklyForecastModel model)
        {
            model.CurrentLocation = _locationService.GetGeolocation(appSettings);
        }

        private void SetCurrentLocationForecast(WeeklyForecastModel model)
        {
            model.WeekForcast = GetWeeklyForecastForZipCode(model);
        }

        private Forecast GetWeeklyForecastForZipCode(WeeklyForecastModel model)
        {
            string zip = model.SearchZip;

            model.DisplayedZip = zip;
            model.SearchZip = null;

            return _forecastService.GetWeeklyForecastForZipCode(appSettings, zip);
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
        #endregion View Model Manipulations
    }
}
