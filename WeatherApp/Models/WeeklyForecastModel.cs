using Microsoft.AspNetCore.Mvc.Rendering;
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
        public GeolocationModel CurrentLocation { get; set; }
        public Forecast WeekForcast { get; set; }
        public string SearchZip { get; set; }
        public string SearchZipHistory { get; set; }
        public string DisplayedZip { get; set; }
        public List<string> SearchHistory { get; set; } = new List<string>();

        public SelectList SearchHistoryItems { get; set;}

        
    }
}
