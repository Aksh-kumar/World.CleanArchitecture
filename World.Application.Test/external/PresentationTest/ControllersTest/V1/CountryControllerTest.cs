using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts.Services;
using World.Application.ResponseDTO.Country;
using World.Domain.DomainEntity.World;
using World.Domain.Entity;
using World.Presentation.Controllers.v1;
using World.Unit.Test.Helper;
using World.Unit.Test.Stubs;
using Xunit.Abstractions;

namespace World.Unit.Test.external.PresentationTest.ControllersTest.V1
{
    public class CountryControllerTest
    {
        private readonly Mock<ICountryService> _countryServiceMock;
        private readonly ILogger<CountryController> _loggerMock;

        public CountryControllerTest(ITestOutputHelper output)
        {
            _loggerMock = new LoggerStubs<CountryController>(output);
            _countryServiceMock = new();
        }

        [Fact]
        public async Task Get_country_By_Name_BadRequest_Test()
        {
            // Arrange
            string countryName = string.Empty;
            var CountryController = new CountryController(_countryServiceMock.Object, _loggerMock);
            
            // Act
            BadRequestResult? result = await CountryController.Get(countryName, default) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Get_country_By_Name_Success_Test()
        {
            // Arrange
            string countryName = "Test";
            GetCountryResponse response = DummyObject.GetCountryResponseDummy(countryName);

            _ = _countryServiceMock.Setup(
                x => x.GetCountryByName(It.IsAny<string>(), It.IsAny<CancellationToken>())
                ).ReturnsAsync(response);

            var CountryController = new CountryController(_countryServiceMock.Object, _loggerMock);
            // Act

            OkObjectResult? result = await CountryController.Get(countryName, default) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value as GetCountryResponse);
            Assert.Equal(countryName, (result.Value as GetCountryResponse).Name);
        }
    }
}
