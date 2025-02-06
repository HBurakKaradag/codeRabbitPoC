using Microsoft.AspNetCore.Mvc;

namespace PocOfCodeRabbit.Api.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            try
            {
                var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToArray();

                return data;
            }
            finally
            {
                // Summaries = null;
            }
        }

        private static Random random = new Random();

        private static List<WeatherData> weatherForecasts = new List<WeatherData>();

        [HttpGet("forecast")]
        public ActionResult<List<WeatherData>> GetWeatherForecasts()
        {
            if (weatherForecasts.Count == 0)
            {
                string[] cities = { "Ýstanbul", "Ankara", "Ýzmir", "Bursa", "Antalya", "Adana", "Konya", "Trabzon", "Erzurum", "Gaziantep" };

                foreach (var city in cities)
                {
                    var forecast = new WeatherData
                    {
                        City = city,
                        Temperature = random.Next(-5, 40),
                        Condition = GetRandomCondition()
                    };

                    weatherForecasts.Add(forecast);
                }
            }
            else
            {
                foreach (var forecast in weatherForecasts)
                {
                    forecast.Temperature = random.Next(-5, 40);
                    forecast.Condition = GetRandomCondition();
                }
            }

            return Ok(weatherForecasts);
        }

        private string GetRandomCondition()
        {
            string[] conditions = { "Güneþli", "Yaðmurlu", "Karlý", "Bulutlu", "Rüzgarlý" };
            return conditions[random.Next(conditions.Length)];
        }
    }
}

    public class WeatherData
    {
        public string City { get; set; }
        public int Temperature { get; set; }
        public string Condition { get; set; }
    }
