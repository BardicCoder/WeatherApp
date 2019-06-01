using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdPartyApiCaller.Factory;
using ThirdPartyApiCaller.Models;
using ThirdPartyApiCaller.Utility;

namespace WeatherApp.Models
{
    public class WeeklyForecastModel
    {
        public GeolocationModel CurrentLocation { get; private set; }
        public Forecast WeekForcast { get; private set; }
        private readonly IOptions<MySettingsModel> appSettings;

        public WeeklyForecastModel(IOptions<MySettingsModel> settings)
        {
            appSettings = settings;
            Task<bool> location = SetCurrentLocation();
            location.Wait();

            Task<bool> forecast = SetCurrentLocationForecast();
            forecast.Wait();
            
        }

        private async Task<bool> SetCurrentLocation()
        {
            CurrentLocation = await ApiClientFactory.Instance.CallGeolocationApi(appSettings.Value.GeolocationUrl);
            return true;
        }

        private async Task<bool> SetCurrentLocationForecast()
        {
            //while(CurrentLocation == null)
            //{
            //    //do nothing, just wait.
            //}
            await GetWeeklyForecastForZipCode(CurrentLocation.ZipCode);
            return true;
        }

        public async Task<Forecast> GetWeeklyForecastForZipCode(string zip)
        {
            appSettings.Value.WeatherZip = zip;
            WeekForcast = await ApiClientFactory.Instance.CallWeatherApi(appSettings.Value.WeatherUrl);
            return WeekForcast;
        }
    }
}
