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

        [HttpGet("GetWeatherForecast")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var forecast = new WeatherForecast
                {
                    Date = DateTime.Now,
                    TemperatureC = Random.Shared.Next(-25, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]

                };

                var reccord = new WeatherForecastReccord()
                {
                    Forecast = JsonConvert.SerializeObject(forecast),
                    CreatedOn = DateTime.Now.ToUniversalTime(),
                };

                //var result = await this.dbContext.WeatherForecastReccords.AddAsync(reccord);
                //var result2 = await this.dbContext.SaveChangesAsync();

                var newId = this.SaveInLiteDb(forecast);

                return Ok(forecast);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }        
        }

        private int SaveInLiteDb(WeatherForecast forecast)
        {
            using (var db = new LiteDatabase(@"C:\dev\Simple.Api\Simple.Api\Simple.Api\LiteDb\SimpleData.db"))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<WeatherForecast>("forecasts");

                col.Insert(forecast);

                return forecast.Id;
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