using MediatR;
using World.Application.Contracts.Messaging;
using World.Application.ResponseDTO.Country;

namespace World.Application.Country.Command;

public sealed record CreateCountryCommand
   (
       string? HeadOfState,
       int? Capital,
       decimal SurfaceArea,
       short? IndepYear,
       int Population,
       decimal? LifeExpectancy,
       decimal? Gnp,
       decimal? Gnpold,
       string Code = null!,
       string Name = null!,
       string Continent = null!,
       string Region = null!,
       string LocalName = null!,
       string GovernmentForm = null!,
       string Code2 = null!
   ) : ICommand<GetCountryResponse>
{
}