using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Contract.BaseRepository;
using World.Domain.DomainEntity.World;

namespace World.Domain.Contract.Write
{
    public interface ICountryWriteRepository : IWriteRepository<Country>
    {
        /// <summary>
        /// Add new Country Entity to DB
        /// </summary>
        /// <param name="country"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Country?> AddCountry(Country country, CancellationToken cancellationToken);
        /// <summary>
        /// Update 1 Country Entity To DB
        /// </summary>
        /// <param name="country"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Country?> UpdateCountry(Country country, CancellationToken cancellationToken);
        /// <summary>
        /// Delete 1 Country Entity To DB
        /// </summary>
        /// <param name="country"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Country?> DeleteCountry(Country country, CancellationToken cancellationToken);
        /// <summary>
        /// Delete country By Country Code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteCountry(string countryCode, CancellationToken cancellationToken);
    }
}
