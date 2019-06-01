﻿using System;
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
        public IActionResult Index([FromForm] WeeklyForecastModel model)
        {
            forecastModel = new WeeklyForecastModel(appSettings);

            Task t = forecastModel.GetWeeklyForecastForZipCode(model.SearchZip);
            t.Wait();

            return View(forecastModel);
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
    }
}
