using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Behaviors;
using World.Application.Contracts.Services;
using World.Domain.Entity;

namespace World.Application.WeatherForecast.Services
{
    public class WeatherForecastService : DisposableBehavior, IWeatherService
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly ILogger<WeatherForecastService> _logger;

        public WeatherForecastService(ILogger<WeatherForecastService> logger)
        {
            _logger = logger;
        }
        public async Task<List<Domain.Entity.WeatherForecast>> getWeatherForcast()
        {
            _logger.LogInformation("coming to  getWeatherForcast method");
            var result = Enumerable.Range(1, 5).Select(index => new Domain.Entity.WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
            _logger.LogInformation("successfully generated 5 random temperature");
            return await Task.Run(() => result.ToList<Domain.Entity.WeatherForecast>());
        }
    }
}
