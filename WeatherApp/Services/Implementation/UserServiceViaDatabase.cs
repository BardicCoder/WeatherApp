using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Data;

namespace WeatherApp.Services
{
    public class UserServiceViaDatabase : IUserService
    {
        public List<SearchHistory> GetUserSearchHistory(dbContext context, int memberId)
        {
            Task<List<SearchHistory>> history = context.UserHistory.Where(member => member.MemberId == memberId).ToListAsync();
            history.Wait();

            return history.Result;
        }

        public void SaveZipCodeToSearchHistory(dbContext context, int memberId, string zipCode)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var check = context.UserHistory.Where(zip => zip.ZipCode.Equals(zipCode) && zip.MemberId == memberId);
                    //ensure data integrity.
                    if (check.Count<SearchHistory>() == 0)
                    {
                        SearchHistory item = new SearchHistory() { MemberId = memberId, ZipCode = zipCode };
                        context.Add(item);

                        Task task = context.SaveChangesAsync();
                        task.Wait();

                        transaction.Commit();
                    }

                }

                catch (DbUpdateConcurrencyException)
                {
                    transaction.Rollback();
                    throw;
                }


            }
        }
    }
}
