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
        private dbContext _context;
        public UserServiceViaDatabase(dbContext context)
        {
            _context = context;
        }

        public List<SearchHistory> GetUserSearchHistory(int memberId)
        {
            Task<List<SearchHistory>> history = _context.UserHistory.Where(member => member.MemberId == memberId).ToListAsync();
            history.Wait();

            return history.Result;
        }

        public void SaveZipCodeToSearchHistory(int memberId, string zipCode)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var check = _context.UserHistory.Where(zip => zip.ZipCode.Equals(zipCode) && zip.MemberId == memberId);
                    //ensure data integrity.
                    if (check.Count<SearchHistory>() == 0)
                    {
                        SearchHistory item = new SearchHistory() { MemberId = memberId, ZipCode = zipCode };
                        _context.Add(item);

                        Task task = _context.SaveChangesAsync();
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
