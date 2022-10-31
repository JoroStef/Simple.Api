using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Simple.Api.Services;
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

        private readonly string dbCollection = "weatherforecasts";
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IGenericService genericService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IGenericService genericService)
        {
            _logger = logger;
            this.genericService = genericService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] WeatherForecast weatherForecast)
        {
            try
            {
                weatherForecast.Date = DateTime.Now;

                this.genericService.SaveInLiteDb<WeatherForecast>(weatherForecast, this.dbCollection);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }        
        }

        [HttpGet]
        public IActionResult Read([FromBody] int[] ids)
        {
            try
            {

                var items = this.genericService.GetFromLiteDb<WeatherForecast>(ids, this.dbCollection);

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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