using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Country.Command;
using World.Application.ResponseDTO.Country;
using World.Domain.Shared;
using World.Presentation.Controllers.v2;
using World.Unit.Test.Helper;
using World.Unit.Test.Stubs;
using Xunit.Abstractions;

namespace World.Unit.Test.external.PresentationTest.ControllersTest.V2
{
    public class CountryControllerTest
    {
        private readonly Mock<ISender> _senderMock;
        private readonly ILogger<CountryController> _loggerMock;
        public CountryControllerTest(ITestOutputHelper output)
        {
            _loggerMock = new LoggerStubs<CountryController>(output);
            _senderMock = new Mock<ISender>();
        }

        [Fact]
        public async Task Add_Request_Give_500_Error_Test()
        {
            // Arrange
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            CreateCountryCommand addCountryCommand = null;
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            Result<GetCountryResponse> response = Result.Create<GetCountryResponse>(null);
            _ = _senderMock.Setup(
               x => x.Send(It.IsAny<CreateCountryCommand>(), It.IsAny<CancellationToken>())
               ).ReturnsAsync(response);
            
            var countryController = new CountryController(_senderMock.Object, _loggerMock);

            // Act
            
            #pragma warning disable CS8604 // Possible null reference argument.
            ObjectResult? result = await countryController.Add(addCountryCommand, default) as ObjectResult;
            #pragma warning restore CS8604 // Possible null reference argument.

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal(Error.NullValue.Code, (result.Value as Error).Code);
        }

        [Fact]
        public async Task Add_Request_Give_200_Success_Test()
        {
            // Arrange
            CreateCountryCommand addCountryCommand = DummyObject.GetCreateCountryCommandDummy();
            GetCountryResponse responseObj = DummyObject.GetCountryResponseDummy();
            Result<GetCountryResponse> response = Result.Success<GetCountryResponse>(responseObj);

            _ = _senderMock.Setup(
               x => x.Send(It.IsAny<CreateCountryCommand>(), It.IsAny<CancellationToken>())
               ).ReturnsAsync(response);

            var countryController = new CountryController(_senderMock.Object, _loggerMock);

            // Act
            
            ObjectResult? result = await countryController.Add(addCountryCommand, default) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(responseObj.Name, (result.Value as Result<GetCountryResponse>).Value.Name);
        }
    }
}
