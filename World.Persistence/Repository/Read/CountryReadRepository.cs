using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using World.Persistence.Repository.Base;
using World.Domain.Contract.BaseRepository;
using World.Domain.DomainEntity.World;
using AutoMapper;
using World.Domain.Contract;
using World.Persistence.Contracts;
using World.Domain.Contract.Read;
using Microsoft.Extensions.Logging;

namespace World.Persistence.Repository.Read
{
    internal class CountryReadRepository : ReadRepository<Country>, ICountryReadRepository
    {
        private readonly ILogger<ICountryReadRepository> _logger;
        public CountryReadRepository(IUnitOfWorkWorldDb unitOfWork, ILogger<ICountryReadRepository> logger) : base(unitOfWork)
        {
            _logger = logger;
        }
        public async Task<Country?> SelectCountryByName(string name, CancellationToken cancellationToken = default)
        {
            
            _logger.LogInformation($"Fetching Country  {name} Details from Country Repository");
            StopTracking();
            List<Country> country = await SelectAsync(x => x.Name == name, cancellationToken);
            if (country.Count == 1) 
            {
                _logger.LogInformation($"Successfully found 1 Country with name {name}");
                return country.Single();
            }
            _logger.LogInformation($"No Country found with given Name {name}");
            return null;
        }
    }
}
