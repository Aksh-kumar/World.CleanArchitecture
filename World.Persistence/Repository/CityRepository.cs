using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Contract;
using World.Domain.Contract.BaseRepository;
using World.Persistence.Repository.Base;
using World.Domain.DomainEntity.World;
using AutoMapper;
using World.Persistence.Contracts;

namespace World.Persistence.Repository
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        #region Private variable
        #endregion
        #region constructor
        public CityRepository(IUnitOfWorkWorldDb unitOfWork) : base(unitOfWork)
        {
        }
        #endregion
        #region private
        #endregion
        #region public
        public async Task<City?> GetCityById(int Id)
        {
            IList<City> cities = await SelectAsync(x => x.Id == Id);
            if (cities.Count == 1)
            {
                return cities.Single();
            }
            return null;
        }
        public async Task<City?> AddCity(City City)
        {
            return await Add(City);
        }
        public async Task<City?> DeleteCity(City City)
        {
            return await Delete(City);
        }
        public async Task<City?> UpdateCity(City City)
        {
            return await Update(City);
        }
        #endregion
    }
}