using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Contract.BaseRepository;
using World.Domain.Contract.Read;
using World.Domain.Contract.Write;
using World.Domain.DomainEntity.World;
using World.Persistence.Contracts;
using World.Persistence.Repository.Base;

namespace World.Persistence.Repository.Write
{
    internal class CountryWriteRepository : WriteRepository<Country>, ICountryWriteRepository
    {
        private readonly ICountryReadRepository _countryReadRepository;
        private readonly ILogger<ICountryWriteRepository> _logger;
        public CountryWriteRepository(
            IUnitOfWorkWorldDb unitOfWork,
            ICountryReadRepository countryReadRepository,
            ILogger<ICountryWriteRepository> logger) : base(unitOfWork)
        {
            _logger = logger;
            _countryReadRepository = countryReadRepository;
        }

        public async Task<Country?> AddCountry(Country country, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Adding Country {country.Name} Entity to Database");
             await AddAsync(country, cancellationToken);
             int count = await _unitOfWork.SaveChangesAsync(cancellationToken);
             return (count == 1) ? country : null;
        }
        public async Task<Country?> UpdateCountry(Country country, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Updating Country {country.Name} Entity to Database");
            await UpdateAsync(country, cancellationToken);
            int count = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return (count == 1) ? country : null;
        }
        public async Task<Country?> DeleteCountry(Country country, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Delete Country {country.Name} Entity to Database");
            await DeleteAsync(country, cancellationToken);
            int count = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return (count == 1) ? country : null;
        }

        public async Task<bool> DeleteCountry(string countryCode, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Deleting Country By Code {countryCode} Entity to Database");
            var countries = await _countryReadRepository.SelectAsync(x => x.Code == countryCode, cancellationToken);
            var country = countries.Count == 1 ? countries.First() : null;
            if (country == null) return false;
            var c = await DeleteCountry(country, cancellationToken);
            return c != null;
        }
    }
}
