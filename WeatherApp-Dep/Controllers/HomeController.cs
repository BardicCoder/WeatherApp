using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ThirdPartyApiCaller.Factory;
using ThirdPartyApiCaller.Utility;
using WeatherApp.Models;


namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;
        private WeeklyForecastModel forecastModel;

        public HomeController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.GeolocationUrl;
        }

        public IActionResult Index()
        {
            forecastModel = new WeeklyForecastModel(appSettings);
            return View(forecastModel);
        }

        [HttpPost]
        public void UpdateZip()
        {
            Task updatedForecast = forecastModel.GetWeeklyForecastForZipCode("99123");
            updatedForecast.Wait();
        }
    }
}