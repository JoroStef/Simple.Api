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
            // @"Server=127.0.0.1;Port=5432;Database=SimpleDb;User Id=postgres;Password=password;"
            // @"Database=simpledb; Data Source=pgserver123456789.postgres.database.azure.com; User Id=myadmin@pgserver123456789; Password=123pG!321"
            optionsBuilder.UseNpgsql(@"Database=simpledb; Server=pgserver123456789.postgres.database.azure.com; User Id=myadmin@pgserver123456789; Password=123pG!321;");
        }
    }
}
