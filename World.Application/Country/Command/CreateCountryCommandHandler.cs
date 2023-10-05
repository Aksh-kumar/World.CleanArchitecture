using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts.Messaging;
using DomainEnt = World.Domain.DomainEntity.World;
using World.Domain.Shared;
using World.Application.ResponseDTO.Country;
using System.Runtime.CompilerServices;
using World.Application.Behaviors;
using Microsoft.Extensions.Logging;
using World.Domain.Contract.Read;
using World.Domain.Contract.Write;

// [assembly: InternalsVisibleTo("World.Test")]
namespace World.Application.Country.Command
{
    internal sealed class CreateCountryCommandHandler : ICommandHandler<CreateCountryCommand, GetCountryResponse>
    {
        private readonly ICountryWriteRepository _countryWriteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCountryCommandHandler> _logger;

        public CreateCountryCommandHandler(
            ICountryWriteRepository countryWriteRepository,
            IMapper mapper,
            ILogger<CreateCountryCommandHandler> logger)
        {
            this._countryWriteRepository = countryWriteRepository;
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
                _logger.LogError("cannot create country object from CreateCountryCommand Object");
                return Result.Failure<GetCountryResponse>(Error.NullValue);
            }
            _logger.LogInformation("Adding country to Database {allProperty}", country.GetAllProperties());
            country = await _countryWriteRepository.AddCountry(country, cancellationToken);
            if(country is null)
            {
                _logger.LogError("getting null value after insertion into database");
                return Result.Failure<GetCountryResponse>(Error.NullValueAfterInsertion);
            }
            _logger.LogError("successfully inserted country into databse");
            return Result.Success(_mapper.Map<DomainEnt.Country?, GetCountryResponse>(country));
        }
    }
}
