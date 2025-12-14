using Microsoft.AspNetCore.Mvc;

namespace MyFirstApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(
                index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }

        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast forecast)
        {
            // Add data to storage (e.g., database)
            return Ok(forecast);
        }

        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] WeatherForecast forecast)
        //{
        //    // Update data for the given ID
        //    // Example: Find and update an item with a matching ID
        //    var existingForecast.Date   /* fetch the data */;
        //    existingForecast.Date = forecast.Date;

        //return NoContent();
        //}

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Delete data for the given ID
            return NoContent();
        }
    }
}
