using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ThirdPartyApiCaller.Factory;
using ThirdPartyApiCaller.Models;
using ThirdPartyApiCaller.Utility;

namespace WeatherApp.Services
{
    public class WeatherForecastServiceViaWeb : IWeatherForecastService
    {
        public Forecast GetWeeklyForecastForZipCode(MySettingsModel settings, string zipCode)
        {
            settings.WeatherZip = zipCode;
            Task<Forecast> forecast = ApiClientFactory.Instance.CallWeatherApi(settings.WeatherUrl);
            forecast.Wait();

            return forecast.Result;
        }
    }
}
