using System.Text.Json.Serialization;

namespace Simple.Data.Models
{
    public class WeatherForecast
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("p-date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("p-tempC")]
        public int TemperatureC { get; set; }

        [JsonPropertyName("p-tempF")]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [JsonPropertyName("p-summary")]
        public string? Summary { get; set; }
    }
}