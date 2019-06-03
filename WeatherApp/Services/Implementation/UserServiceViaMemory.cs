using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Data;

namespace WeatherApp.Services
{
    public class UserServiceViaMemory : IUserService
    {
        public List<string> zipCodeHistory { get; set; } = new List<string>();

        public List<SearchHistory> GetUserSearchHistory(int memberId)
        {
            List<SearchHistory> history = new List<SearchHistory>();
            foreach(string zip in zipCodeHistory)
            {
                history.Add(new SearchHistory() { MemberId = 1, ZipCode = zip });
            }
            return history;
        }

        public void SaveZipCodeToSearchHistory(int memberId, string zipCode)
        {
            zipCodeHistory.Add(zipCode);
        }
    }
}
