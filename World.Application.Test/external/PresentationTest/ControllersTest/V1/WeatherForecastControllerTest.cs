using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts.Services;
using World.Application.Country.Command;
using World.Domain.Entity;
using World.Presentation.Controllers.v1;
using World.Unit.Test.Stubs;
using Xunit.Abstractions;

namespace World.Unit.Test.external.PresentationTest.ControllersTest.V1
{
    public class WeatherForecastControllerTest
    {
        private readonly Mock<IWeatherService> _weatherServiceMock;
        private readonly ILogger<WeatherForecastController> _loggerMock;
        public WeatherForecastControllerTest(ITestOutputHelper output)
        {
            _loggerMock = new LoggerStubs<WeatherForecastController>(output); ;
            _weatherServiceMock = new();
        }

        [Fact]
        public async Task Get_Weather_Forecast_Test()
        {
            // Arrange
            List<WeatherForecast> forecast = new List<WeatherForecast>()
            {
                new(){ TemperatureC= 34, Date=DateTime.Now, Summary="First Entry"},
                new(){ TemperatureC= 38, Date=DateTime.Now, Summary="Second Entry"}
            };

            var weatherController = new WeatherForecastController(
                _loggerMock,
                _weatherServiceMock.Object
            );
            
            _ = _weatherServiceMock.Setup(
                x => x.getWeatherForcast()
                ).ReturnsAsync(forecast);


            // Act
            OkObjectResult? result = await weatherController.Get() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value as List<WeatherForecast>);
            Assert.Equal(2, (result.Value as List<WeatherForecast>).Count);
        }
    }
}
