using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Simple.Data.Models;

namespace Simple.Data
{
    public class SimpleDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public SimpleDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<WeatherForecastReccord> WeatherForecastReccords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(this.configuration["ConnectionStrings:simpleDb"]);
        }
    }
}
