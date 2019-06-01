using System;
using System.Threading;
using ThirdPartyApiCaller.Clients;
using ThirdPartyApiCaller.Utility;

namespace ThirdPartyApiCaller.Factory
{
    public static class ApiClientFactory
    {
        private static Uri apiUri;

        private static Lazy<ApiClient> apiClient = new Lazy<ApiClient>(
            () => new ApiClient(apiUri),
            LazyThreadSafetyMode.ExecutionAndPublication);

        static ApiClientFactory()
        {
            apiUri = new Uri(ApplicationSettings.WebApiUrl);
        }

        public static ApiClient Instance
        {
            get
            {
                return apiClient.Value;
            }
        }


    }
}
