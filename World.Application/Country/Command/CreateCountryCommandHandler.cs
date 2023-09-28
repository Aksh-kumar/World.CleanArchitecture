using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts.Messaging;
using World.Domain.Contract;
using DomainEnt = World.Domain.DomainEntity.World;
using World.Domain.Shared;
using World.Application.ResponseDTO.Country;
using System.Runtime.CompilerServices;
using World.Application.Behaviors;
using Microsoft.Extensions.Logging;

// [assembly: InternalsVisibleTo("World.Test")]
namespace World.Application.Country.Command
{
    internal sealed class CreateCountryCommandHandler : ICommandHandler<CreateCountryCommand, GetCountryResponse>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCountryCommandHandler> _logger;

        public CreateCountryCommandHandler(
            ICountryRepository countryRepository,
            IMapper mapper,
            ILogger<CreateCountryCommandHandler> logger)
        {
            this._countryRepository = countryRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<Result<GetCountryResponse>> Handle(
            CreateCountryCommand? request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation("create country request handler with requests {allProperty}", request.GetAllProperties());
            DomainEnt.Country? country = _mapper.Map<CreateCountryCommand?, DomainEnt.Country?>(request);
            if (country is null)
            {
                _logger.LogInformation("country object found to be null returning Null value after insertion");
                return Result.Failure<GetCountryResponse>(Error.NullValueAfterInsertion);
            }
            _logger.LogInformation("Adding country to Database {allProperty}", country.GetAllProperties());
            country = await _countryRepository.AddCountry(country, cancellationToken);
            return Result.Success(_mapper.Map<DomainEnt.Country?, GetCountryResponse>(country));
        }
    }
}
