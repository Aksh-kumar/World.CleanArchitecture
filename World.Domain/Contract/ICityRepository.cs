using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using World.Domain.DomainEntity.World;

namespace World.Domain.Contract
{
    public interface ICityRepository
    {
        /// <summary>
        /// Add City Object
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        Task<City?> AddCity(City city);
        /// <summary>
        /// Delete City By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<City?> DeleteCity(City city);
        /// <summary>
        /// Update City object
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        Task<City?> UpdateCity(City city);
        /// <summary>
        /// Return City By ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<City?> GetCityById(int Id);
    }
}
