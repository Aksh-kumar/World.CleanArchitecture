using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using World.Application.Behaviors;
using World.Application.Contracts.Messaging;
using World.Application.ResponseDTO.Country;
using World.Domain.Contract.Read;
using World.Domain.Contract.Write;
using World.Domain.Shared;
using DomainEnt = World.Domain.DomainEntity.World;

namespace World.Application.Country.Query
{
    public class GetCountryByNameQueryHandler : IQueryHandler<GetCountryByNameQuery, GetCountryResponse>
    {
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly ILogger<GetCountryByNameQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetCountryByNameQueryHandler(
            ICountryReadRepository countryReadRepository,
            ILogger<GetCountryByNameQueryHandler> logger,
            IMapper mapper)
        {
            this._countryReadRepository = countryReadRepository;
            this._mapper = mapper;
            _logger = logger;
        }
        public async Task<Result<GetCountryResponse>> Handle(GetCountryByNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Selecting the corrosponding request by name {request.Name} from repository");
            DomainEnt.Country? country = await _countryReadRepository.SelectCountryByName(
                request.Name, 
                cancellationToken
            );
            GetCountryResponse? response = _mapper.Map< DomainEnt.Country?, GetCountryResponse?>(country);
            if (response is null)
            {
                _logger.LogInformation($"No country found with the given name {request.Name} returning Not Found");
                return Result.Failure<GetCountryResponse>(new Error(
                    "Counry.NotFound",
                    $"Country with given Name {request.Name} is not found")
                    );
            }
            _logger.LogInformation($"Successfully found Country Entity In repos {response.GetAllProperties()}");
            return Result.Success<GetCountryResponse>(response);
        }
    }
}
