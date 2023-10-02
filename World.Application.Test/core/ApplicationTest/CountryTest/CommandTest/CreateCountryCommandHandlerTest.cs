using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Serilog;
using World.Application.Country.Command;
using World.Application.ResponseDTO.Country;
using World.Domain.Contract;
using World.Domain.Shared;
using World.Unit.Test.Dummy;
using World.Unit.Test.Stubs;
using Xunit.Abstractions;
using DomainEnt = World.Domain.DomainEntity.World;

namespace World.Unit.Test.core.ApplicationTest.CountryTest;

public class CreateCountryCommandHandlerTest
{
    private readonly Mock<ICountryRepository> _countryRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ILogger<CreateCountryCommandHandler> _loggerMock;
    public CreateCountryCommandHandlerTest(ITestOutputHelper output)
    {
        _countryRepositoryMock = new();
        _mapperMock = new();
        _loggerMock = new LoggerStubs<CreateCountryCommandHandler>(output);

    }

    [Fact]
    public async Task Create_country_command_null_failure_test()
    {
        // arrange
        CreateCountryCommand? request = null;
        CancellationToken cancellationToken = new();

        var createCountryCommandHandler = new CreateCountryCommandHandler(
            _countryRepositoryMock.Object,
            _mapperMock.Object,
            _loggerMock
        );

        _mapperMock.Setup(

            x => x.Map<CreateCountryCommand, DomainEnt.Country>(It.IsAny<CreateCountryCommand>())
            ).Returns<CreateCountryCommand>(null);

        _ = _countryRepositoryMock.Setup(
            x => x.AddCountry(It.IsAny<DomainEnt.Country>(), It.IsAny<CancellationToken>())
            ).Returns<DomainEnt.Country?>(null);

        // act
        Result<GetCountryResponse> result = await createCountryCommandHandler.Handle(request, cancellationToken);

        // assert
        Assert.True(result.IsFailure);
        Assert.Equal(result.Error.Message, Error.NullValueAfterInsertion.Message);
    }
    [Fact]
    public async Task Create_country_command_success_test()
    {
        // arrange
        CreateCountryCommand request = DummyObject.GetCreateCountryCommandDummy();
        DomainEnt.Country response = DummyObject.GetCountryDummy(request);
        
        CancellationToken cancellationToken = new();
        var createCountryCommandHandler = new CreateCountryCommandHandler(
            _countryRepositoryMock.Object,
            _mapperMock.Object,
            _loggerMock
        );

        _ = _mapperMock.Setup(

            x => x.Map<CreateCountryCommand, DomainEnt.Country?>(It.IsAny<CreateCountryCommand>())
            ).Returns<CreateCountryCommand>(_ => response);

        _ = _mapperMock.Setup(
             x => x.Map<DomainEnt.Country?, GetCountryResponse?>(It.IsAny<DomainEnt.Country>())
            ).Returns<DomainEnt.Country?>(DummyObject.GetCountryResponseDummy);

        _ = _countryRepositoryMock.Setup(
            x => x.AddCountry(It.IsAny<DomainEnt.Country>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(response);

        // act
        Result<GetCountryResponse> result = await createCountryCommandHandler.Handle(request, cancellationToken);

        // assert
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.HeadOfState, response.HeadOfState);
        Assert.Equal(result.Value.Capital, response.Capital);
        Assert.Equal(result.Value.SurfaceArea, response.SurfaceArea);
        Assert.Equal(result.Value.IndepYear, response.IndepYear);
        Assert.Equal(result.Value.Population, response.Population);
        Assert.Equal(result.Value.LifeExpectancy, response.LifeExpectancy);
        Assert.Equal(result.Value.Gnp, response.Gnp);
        Assert.Equal(result.Value.Gnpold, response.Gnpold);
    }
}