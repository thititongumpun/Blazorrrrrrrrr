using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceBlazorWeb.Data
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly string[] Locations = new[]
        {
            "Bangkok", "Nonthaburi", "Chaingmai", "HatYai", "Rayong", "Ranong", "Chonburi", "HuaHin", "Home"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Location = Locations[rng.Next(Locations.Length)],
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}
