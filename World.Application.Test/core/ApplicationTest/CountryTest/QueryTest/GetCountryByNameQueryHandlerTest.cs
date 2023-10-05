using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Country.Command;
using World.Application.Country.Query;
using World.Application.ResponseDTO.Country;
using World.Domain.Contract.Read;
using World.Domain.DomainEntity.World;
using World.Unit.Test.Helper;
using World.Unit.Test.Stubs;
using Xunit.Abstractions;

namespace World.Unit.Test.core.ApplicationTest.CountryTest.QueryTest
{
    public class GetCountryByNameQueryHandlerTest
    {
        private readonly Mock<ICountryReadRepository> _countryReadRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ILogger<GetCountryByNameQueryHandler> _logger;
        public GetCountryByNameQueryHandlerTest(ITestOutputHelper output)
        {
            _countryReadRepositoryMock = new();
            _mapperMock = new();
            _logger = new LoggerStubs<GetCountryByNameQueryHandler>(output);
        }
        [Fact]
        public async Task Get_Country_By_Name_Not_found_Test()
        {
            // Arrange
            CancellationToken cancellationToken = new();
            GetCountryByNameQuery query = new("Sample Country");
            GetCountryByNameQueryHandler handler = new(
                _countryReadRepositoryMock.Object,
                _logger,
                _mapperMock.Object
            );
            _ = _mapperMock.Setup(
                x => x.Map<Country, GetCountryResponse>(It.IsAny<Country>())
            ).Returns<GetCountryResponse?>((res) => null);

            _ = _countryReadRepositoryMock.Setup(
                x => x.SelectCountryByName(It.IsAny<string>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync<ICountryReadRepository, Country?>(() => null);

            // Act
            var result = await handler.Handle(query, cancellationToken);
            
            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Counry.NotFound", result.Error.Code);
        }

        [Fact]
        public async Task Get_Country_By_Name_Success_Test()
        {
            // Arrange
            CancellationToken cancellationToken = new();
            GetCountryByNameQuery query = new("Sample Country");
            Country? countryDummy = DummyObject.GetCountryDummy(countryName: query.Name);

            GetCountryByNameQueryHandler handler = new(
                _countryReadRepositoryMock.Object,
                _logger,
                _mapperMock.Object
            );
            _ = _mapperMock.Setup(
             x => x.Map<Country?, GetCountryResponse?>(It.IsAny<Country>())
            ).Returns<Country?>(DummyObject.GetCountryResponseDummy);

            _ = _countryReadRepositoryMock.Setup(
                x => x.SelectCountryByName(It.IsAny<string>(), It.IsAny<CancellationToken>())
            ).ReturnsAsync<ICountryReadRepository, Country?>(() => countryDummy);

            // Act
            var result = await handler.Handle(query, cancellationToken);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(query.Name, result.Value.Name);
        }
    }

}
