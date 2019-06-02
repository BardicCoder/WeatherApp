using System;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Data
{
    public class dbContextFactory
    {
        public dbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<dbContext>();

            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            builder.UseSqlServer(config.GetConnectionString("WeatherAppDb"));

            return new dbContext(builder.Options);
        }
    }
}
