using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Country.Command;
using World.Application.ResponseDTO.Country;
using World.Domain.DomainEntity.World;

namespace World.Unit.Test.Dummy;

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
        string code = "TST"
    )
    {
        Country country = new();
        country.HeadOfState = "sample head of state";
        country.Capital = 10;
        country.Name = countryName;
        country.SurfaceArea = 123.34m;
        country.IndepYear = 1993;
        country.Population = 8982182;
        country.LifeExpectancy = 78.4m;
        country.Gnp = 12.3m;
        country.Gnpold = 3232.2m;
        country.Code = code;
        return country;
    }
}


