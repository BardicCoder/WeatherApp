using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdPartyApiCaller.Models;
using ThirdPartyApiCaller.Utility;

namespace WeatherApp.Services
{
    public interface ILocationService
    {
         GeolocationModel GetGeolocation(IOptions<MySettingsModel> settings);
    }
}
