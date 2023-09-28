using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using World.Domain.DomainEntity.World;

namespace World.Domain.Contract
{
    public interface ICountryRepository : IDisposable
    {
        /// <summary>
        /// Select Country By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Country?> SelectCountryByName(string name, CancellationToken cancellationToken);
        /// <summary>
        /// Add new Country Entity to DB
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        Task<Country?> AddCountry(Country country, CancellationToken cancellationToken);
        /// <summary>
        /// Update Country Entity To DB
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        Task<Country?> UpdateCountry(Country country, CancellationToken cancellationToken);
        /// <summary>
        /// Delete Country Entity To DB
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        Task<Country?> DeleteCountry(Country country, CancellationToken cancellationToken);
        /// <summary>
        /// Delete country By Country Code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        Task<bool> DeleteCountry(string countryCode, CancellationToken cancellationToken);
    }
}
