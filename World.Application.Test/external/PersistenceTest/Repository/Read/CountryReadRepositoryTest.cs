using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Contract.BaseRepository;
using World.Domain.Contract.Read;
using World.Domain.DomainEntity.World;
using World.Persistence.Contracts;
using World.Persistence.DBContext;
using World.Persistence.Repository.Base;
using World.Persistence.Repository.Read;
using World.Unit.Test.Helper;
using World.Unit.Test.Stubs;
using Xunit.Abstractions;

namespace World.Unit.Test.external.PersistenceTest.Repository.Read
{
    public class CountryReadRepositoryTest : BaseRepositoryTest<Country, ICountryReadRepository>
    {
        private readonly ICountryReadRepository _countryReadRepository;
        public CountryReadRepositoryTest(ITestOutputHelper output) : base(output)
        {
            _countryReadRepository = new CountryReadRepository(unitOfWorkWorldDb, logger);
        }

        [Fact]
        public async Task Select_All_Test()
        {
            // Arrange
            string countryName = "Sample select All test";
            string code = "SML";
            Country country = DummyObject.GetCountryDummy(
                countryName: countryName,
                code: code
            );
            AddEntity(country);

            // Act
            var countries = await _countryReadRepository.SelectAllAsync(default);
            var obj = countries.Where(x => x.Name == countryName);
            
            // assert
            Assert.Single(obj);
        }

        [Fact]
        public async Task Select_Test()
        {
            // Arrange
            string countryName = "Sample select Func test";
            string code = "SFL";
            Country country = DummyObject.GetCountryDummy(
                countryName: countryName,
                code: code
            );
            AddEntity(country);

            // Act
            var countries = await _countryReadRepository.SelectAsync(
                x => x.Name == countryName,
                default
            );

            // assert
            Assert.Single(countries);
        }

        [Fact]
        public async Task Select_Country_By_Name_Success_Test()
        {
            // Arrange
            string countryName = "Sample select By Name test";
            string code = "SSL";
            Country country = DummyObject.GetCountryDummy(
                countryName: countryName,
                code: code
            );
            AddEntity(country);

            // Act
            var result = await _countryReadRepository.SelectCountryByName(
                countryName,
                default
            );
            
            // assert
            Assert.NotNull(result);
            Assert.Equal(countryName, result.Name);

        }

        [Fact]
        public async Task Select_Country_By_Name_Failure_Test()
        {
            // Arrange
            string invalidCountry = "Not A Country";
            
            // Act
            var country = await _countryReadRepository.SelectCountryByName(
                invalidCountry,
                default
            );
            
            // assert
            Assert.Null(country);
        }
    }
}
