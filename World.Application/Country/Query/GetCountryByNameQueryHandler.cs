using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using World.Application.Contracts.Messaging;
using World.Application.ResponseDTO.Country;
using World.Domain.Contract;
using World.Domain.Shared;
using DomainEnt = World.Domain.DomainEntity.World;

namespace World.Application.Country.Query
{
    public class GetCountryByNameQueryHandler : IQueryHandler<GetCountryByNameQuery, GetCountryResponse>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetCountryByNameQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            this._countryRepository = countryRepository;
            this._mapper = mapper;
        }
        public async Task<Result<GetCountryResponse>> Handle(GetCountryByNameQuery request, CancellationToken cancellationToken)
        {
            DomainEnt.Country? country = await _countryRepository.SelectCountryByName(request.Name, cancellationToken);
            GetCountryResponse? response = _mapper.Map<GetCountryResponse?>(country);
            if (response is null)
            {
                return Result.Failure<GetCountryResponse>(new Error(
                    "Counry.NotFound",
                    $"Country with given Name {request.Name} is not found")
                    );
            }

            return Result.Success<GetCountryResponse>(response);
        }
    }
}
