using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Simple.Data.Models;

namespace Simple.Data
{
    public class SimpleDbContext : DbContext
    {

        public DbSet<WeatherForecastReccord> WeatherForecastReccords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Server=127.0.0.1;Port=5432;Database=SimpleDb;User Id=postgres;Password=password;");
        }
    }
}
