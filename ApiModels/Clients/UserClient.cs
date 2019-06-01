using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThirdPartyApiCaller.Models;

namespace ThirdPartyApiCaller.Clients
{
    public partial class ApiClient
    {
        public async Task<GeolocationModel> CallGeolocationApi(string apiUrl)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                apiUrl));
            return await GetAsync<GeolocationModel>(requestUrl);
        }

        public async Task<Forecast> CallWeatherApi(string apiUrl)
        {
            //var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
            //    apiUrl));
            return await GetAsync<Forecast>(apiUrl);
        }
    }
}
