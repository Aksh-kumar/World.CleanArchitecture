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

namespace World.Persistence.Repository
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        #region Private variable

        #endregion
        #region constructor
        public CountryRepository(IUnitOfWorkWorldDb unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Country?> SelectCountryByName(string name, CancellationToken cancellationToken = default)
        {
            IList<Country> country = await SelectAsync(x => x.Name == name);
            if (country.Count == 1)
            {
                return country.Single();
            }
            return null;
        }
        public async Task<Country?> AddCountry(Country Country, CancellationToken cancellationToken = default)
        {
            return await Add(Country, cancellationToken);
        }
        public async Task<Country?> UpdateCountry(Country Country, CancellationToken cancellationToken = default)
        {
            return await Update(Country, cancellationToken);
        }
        public async Task<Country?> DeleteCountry(Country Country, CancellationToken cancellationToken = default)
        {
            return await Delete(Country, cancellationToken);
        }

        public async Task<bool> DeleteCountry(string countryCode, CancellationToken cancellationToken = default)
        {
            var countries = await SelectAsync(x => x.Code == countryCode);
            var country = countries.Count == 1 ? countries.First() : null;
            if (country == null) return false;
            var c = await DeleteCountry(country, cancellationToken);
            return c != null;
        }
    }
}
#endregion

