using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;
using System.Configuration;

namespace WeatherApp.Data
{
    public class dbContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SearchHistory> UserHistory { get; set; }

        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Database.GetDbConnection().ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchHistory>()
                .HasKey(item => new { item.MemberId, item.ZipCode });
        }
    }
}
