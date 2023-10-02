using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.ResponseDTO.Country;
using World.Domain.Entity;
using World.Integration.Tests.Utility;

namespace World.Integration.Tests.Controllers.V1
{
    public class WeatherForecastIntegrationTest : BaseIntegrationTesting
    {
        private const string GET_FORECAST_V1_URL = "/api/v1/weatherforecast/get";
        public WeatherForecastIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_should_Return_The_Forecasted_List()
        {
            // Arrange

            // Act
            var getResponse = await client.GetAsync(GET_FORECAST_V1_URL);
            List<WeatherForecast>? getResult = await HttpResponseBody<List<WeatherForecast>>(getResponse);

            // Assert
            Assert.True(getResponse.IsSuccessStatusCode);
            Assert.Equal(5, getResult.Count);
        }
    }
}
