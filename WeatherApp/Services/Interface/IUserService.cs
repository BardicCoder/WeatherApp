using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Data;

namespace WeatherApp.Services
{
    public interface IUserService
    {
        List<SearchHistory> GetUserSearchHistory(int memberId);
        void SaveZipCodeToSearchHistory(int memberId, string zipCode);
    }
}
