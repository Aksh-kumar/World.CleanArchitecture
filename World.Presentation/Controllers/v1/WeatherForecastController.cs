using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using World.Application.Contracts.Services;

namespace World.Presentation.Controllers.v1
{
    public class WeatherForecastController : APIControllerV1Base
    {

        // private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(
            //ILogger<WeatherForecastController> logger,
            IWeatherService weatherService) : base(lists: weatherService)
        {
            //_logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet, Route("[controller]/get")]
        //[MapToApiVersion("1.0")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _weatherService.getWeatherForcast());
        }
    }
}