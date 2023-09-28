using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Behaviors;
using World.Application.Contracts.Services;
using World.Application.ResponseDTO.Country;
using World.Domain.Contract;
using DomainEnt = World.Domain.DomainEntity.World;

namespace World.Application.Country.Services
{
    public class CountryService : DisposableBehavior, ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryService(ICountryRepository countryRepository, IMapper mapper) : base(countryRepository)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<GetCountryResponse?> GetCountryByName([NotNull] string name, CancellationToken cancellationToken)
        {
            DomainEnt.Country? country = await _countryRepository.SelectCountryByName(name, cancellationToken);
            GetCountryResponse? response = _mapper.Map<GetCountryResponse?>(country);
            return response;
        }
    }
}
