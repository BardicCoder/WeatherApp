using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThirdPartyApiCaller.Utility
{
    public class MySettingsModel
    {
        public string GeolocationUrl { get; set; } //{ return "http://ip-api.com/json/"; } }
        public string WeatherKey { get; set; } // { return "0246915cce4426375a5c341a33b6e4a8"; } }
        public string WeatherAppId { get; set; } // { return "1311586d"; } }
        public string WeatherZip { get; set; }
        public string WeatherBaseUrl { get; set; } // { return "http://api.weatherunlocked.com/api/forecast/us.{Zip}?app_id={ID}&app_key={KEY}"; } }
        public string WeatherUrl { get { return WeatherBaseUrl.Replace("{ID}", WeatherAppId).Replace("{KEY}", WeatherKey).Replace("{Zip}", WeatherZip);  } }
    }
}
