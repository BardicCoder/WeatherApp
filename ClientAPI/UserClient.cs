using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThirdPartyApiCaller.Models;

namespace CoreApiClient
{
    public partial class ApiClient
    {
        public async Task<List<GeolocationModel>> CallGeolocationApi(string apiUrl)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                apiUrl));
            return await GetAsync<List<GeolocationModel>>(requestUrl);
        }

        //public async Task<UsersModel> SaveUser(UsersModel model)
        //{
        //    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
        //        "User/SaveUser"));
        //    return await PostAsync<UsersModel>(requestUrl, model);
        //}
    }
}
