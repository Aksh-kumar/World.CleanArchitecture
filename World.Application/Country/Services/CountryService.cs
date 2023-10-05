using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Behaviors;
using World.Application.Contracts.Services;
using World.Application.ResponseDTO.Country;
using World.Domain.Contract.Read;
using DomainEnt = World.Domain.DomainEntity.World;

namespace World.Application.Country.Services
{
    public class CountryService : DisposableBehavior, ICountryService
    {
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly ILogger<ICountryService> _logger;
        private readonly IMapper _mapper;
        public CountryService(
            ICountryReadRepository countryReadRepository,
            ILogger<ICountryService> logger,
            IMapper mapper) : base(countryReadRepository)
        {
            _countryReadRepository = countryReadRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<GetCountryResponse?> GetCountryByName([NotNull] string name, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching Counrty Entity From Repository");
            DomainEnt.Country? country = await _countryReadRepository
                .SelectCountryByName(
                    name,
                    cancellationToken
                );
            _logger.LogInformation($"Country Returned from repository {country.GetAllProperties()}");
            GetCountryResponse? response = _mapper.Map<DomainEnt.Country?, GetCountryResponse?>(country);
            _logger.LogInformation(response.GetAllProperties());
            return response;
        }
    }
}
