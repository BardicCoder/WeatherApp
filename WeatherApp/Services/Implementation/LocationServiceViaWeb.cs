using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdPartyApiCaller.Factory;
using ThirdPartyApiCaller.Models;
using ThirdPartyApiCaller.Utility;

namespace WeatherApp.Services
{
    public class LocationServiceViaWeb : ILocationService
    {
        public GeolocationModel GetGeolocation(MySettingsModel settings)
        {
            Task<GeolocationModel> location = ApiClientFactory.Instance.CallGeolocationApi(settings.GeolocationUrl);
            location.Wait();

            return location.Result;
        }

    }
}
