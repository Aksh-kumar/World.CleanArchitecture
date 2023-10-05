using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts.Services;
using World.Application.Country.Services;
using World.Application.ResponseDTO.Country;
using World.Domain.Contract.Read;
using World.Domain.DomainEntity.World;
using World.Unit.Test.Helper;
using World.Unit.Test.Stubs;
using Xunit.Abstractions;

namespace World.Unit.Test.core.ApplicationTest.CountryTest.ServiceTest
{
    public class CountryServiceTest
    {
        private readonly ILogger<ICountryService> _loggerMock;
        private readonly Mock<ICountryReadRepository> _countryReadRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        public CountryServiceTest(ITestOutputHelper output)
        {
            _loggerMock = new LoggerStubs<ICountryService>(output);
            _mapperMock = new();
            _countryReadRepositoryMock = new();
        }

        [Fact]
        public async Task Get_Country_By_Name_Null_Test()
        {
            // Arrange
            Country? countryDummy = null;
            CancellationToken cancellationToken = new();
            string countryName = "sample country service test";

            _ = _countryReadRepositoryMock.Setup(
                x => x.SelectCountryByName(It.IsAny<string>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync<ICountryReadRepository, Country?>(() => countryDummy);

            _ = _mapperMock.Setup(
                x => x.Map<Country?, GetCountryResponse?>(It.IsAny<Country?>())
            ).Returns<GetCountryResponse?>(null);

            var countryService = new CountryService(
                _countryReadRepositoryMock.Object,
                _loggerMock,
                _mapperMock.Object
                );

            // Act
            var result  = await countryService.GetCountryByName(countryName, cancellationToken);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Get_Country_By_Name_Success_Test()
        {
            // Arrange
            CancellationToken cancellationToken = new();
            string countryName = "sample country service test";
            Country? countryDummy = DummyObject.GetCountryDummy(countryName: countryName);

            _ = _countryReadRepositoryMock.Setup(
                x => x.SelectCountryByName(It.IsAny<string>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync<ICountryReadRepository, Country?>(() => countryDummy);

            _ = _mapperMock.Setup(
             x => x.Map<Country?, GetCountryResponse?>(It.IsAny<Country>())
            ).Returns<Country?>(DummyObject.GetCountryResponseDummy);

            var countryService = new CountryService(
                _countryReadRepositoryMock.Object,
                _loggerMock,
                _mapperMock.Object
                );

            // Act
            var result = await countryService.GetCountryByName(countryName, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(countryName, result.Name);
        }
    }
}
