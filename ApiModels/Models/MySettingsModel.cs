using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThirdPartyApiCaller.Utility
{
    public class MySettingsModel
    {
        public string GeolocationUrl { get { return "http://ip-api.com/json/"; } }
        private string WeatherKey { get { return "0246915cce4426375a5c341a33b6e4a8"; } }
        private string WeatherAppId { get { return "1311586d"; } }
        public string WeatherZip { get; set; }
        private string WeatherBaseUrl { get {return "http://api.weatherunlocked.com/api/forecast/us.{Zip}?app_id={ID}&app_key={KEY}"; }}
        public string WeatherUrl { get { return WeatherBaseUrl.Replace("{ID}", WeatherAppId).Replace("{KEY}", WeatherKey).Replace("{Zip}", WeatherZip);  } }
    }
}
