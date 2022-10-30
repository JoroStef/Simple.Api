using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Simple.Data;
using Simple.Data.Models;

namespace Simple.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SimpleDbContext dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, SimpleDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] WeatherForecast weatherForecast)
        {
            try
            {
                //var forecast = new WeatherForecast
                //{
                //    Date = DateTime.Now,
                //    TemperatureC = Random.Shared.Next(-25, 55),
                //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]

                //};

                //var reccord = new WeatherForecastReccord()
                //{
                //    Forecast = JsonConvert.SerializeObject(forecast),
                //    CreatedOn = DateTime.Now.ToUniversalTime(),
                //};

                //var result = await this.dbContext.WeatherForecastReccords.AddAsync(reccord);
                //var result2 = await this.dbContext.SaveChangesAsync();

                weatherForecast.Date = DateTime.Today;

                var newId = this.SaveInLiteDb(weatherForecast);

                return Ok(newId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }        
        }

        private int SaveInLiteDb(WeatherForecast weatherforecast)
        {
            var connectionString = Directory.GetCurrentDirectory() + "\\LiteDb\\SimpleData.db";
            using (var db = new LiteDatabase(connectionString))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<WeatherForecast>("weatherforecasts");

                col.Insert(weatherforecast);

                return weatherforecast.Id;
            }
        }

        [HttpGet("Random")]
        public async Task<IActionResult> GetRandomAsync()
        {
            try
            {
                var randomNumber = Random.Shared.Next(-100, 100);

                return Ok(randomNumber);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}