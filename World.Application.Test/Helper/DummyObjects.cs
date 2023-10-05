using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Country.Command;
using World.Application.ResponseDTO.Country;
using World.Domain.DomainEntity.World;

namespace World.Unit.Test.Helper;

public static class DummyObject
{
    public static GetCountryResponse GetCountryResponseDummy(
        string countryName = "sample_test",
        string code="TST"
    )
    {
        GetCountryResponse object_ = new()
        {
            HeadOfState = "sample head of state",
            Capital = 10,
            Name = countryName,
            Code = code,
            SurfaceArea = 123.34m,
            IndepYear = 1993,
            Population = 8982182,
            LifeExpectancy = 78.4m,
            Gnp = 12.3m,
            Gnpold = 3232.2m
        };
        return object_;
    }

    public static GetCountryResponse? GetCountryResponseDummy(
        Country? country
    )
    {
        if (country == null)
            return null;
        GetCountryResponse object_ = new GetCountryResponse()
        {
            HeadOfState = country.HeadOfState,
            Capital = country.Capital,
            SurfaceArea = country.SurfaceArea,
            Name = country.Name,
            IndepYear = country.IndepYear,
            Population = country.Population,
            LifeExpectancy = country.LifeExpectancy,
            Gnp = country.Gnp,
            Gnpold = country.Gnpold,
            Code = country.Code
        };
        return object_;
    }

    public static CreateCountryCommand GetCreateCountryCommandDummy(
        string countryName = "sample_test",
        string code = "TST"
    )
    {
        CreateCountryCommand object_ = new(

            HeadOfState: "sample head of state",
            Capital: 10,
            Name: countryName,
            Code: code,
            SurfaceArea: 123.34m,
            IndepYear: 1993,
            Population: 8982182,
            LifeExpectancy: 78.4m,
            Gnp: 12.3m,
            Gnpold: 3232.2m
        );
        return object_;
    }
    public static Country GetCountryDummy(CreateCountryCommand createCountryCommand)
    {
        Country object_ = new();
        object_.HeadOfState = createCountryCommand.HeadOfState;
        object_.Capital = createCountryCommand.Capital;
        object_.Name = createCountryCommand.Name;
        object_.Code = createCountryCommand.Code;
        object_.SurfaceArea = createCountryCommand.SurfaceArea;
        object_.IndepYear = createCountryCommand.IndepYear;
        object_.Population = createCountryCommand.Population;
        object_.LifeExpectancy = createCountryCommand.LifeExpectancy;
        object_.Gnp = createCountryCommand.Gnp;
        object_.Gnpold = createCountryCommand.Gnpold;
        return object_;
    }

    public static Country GetCountryDummy(
        string countryName = "sample_test",
        string code = "TST",
        int? capital = 10,
        decimal surfaceArea = 123.34m,
        short? indepYear = 1993,
        int population = 8982182,
        decimal? lifeExpectancy = 78.4m,
        decimal? gnp = 12.3m,
        decimal? gnpold = 3232.2m,
        string code2="TS",
        string continent="Asia",
        string governmentForm = "Republic of test",
        string localName = "local Test",
        string region = "North East"
    )
    {
        Country country = new();
        country.HeadOfState = "sample head of state";
        country.Capital = capital;
        country.Name = countryName;
        country.SurfaceArea = surfaceArea;
        country.IndepYear = indepYear;
        country.Population = population;
        country.LifeExpectancy = lifeExpectancy;
        country.Gnp = gnp;
        country.Gnpold = gnpold;
        country.Code = code;
        country.Code2 = code2;
        country.Continent = continent;
        country.GovernmentForm = governmentForm;
        country.LocalName = localName;
        country.Region = region;
        return country;
    }
}


