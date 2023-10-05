using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Contract.Read;
using World.Domain.Contract.Write;
using World.Domain.DomainEntity.World;
using World.Persistence.Repository.Read;
using World.Persistence.Repository.Write;
using World.Unit.Test.Helper;
using World.Unit.Test.Stubs;
using Xunit.Abstractions;

namespace World.Unit.Test.external.PersistenceTest.Repository.Write
{
    public class CountryWriteRepositoryTest : BaseRepositoryTest<Country, ICountryWriteRepository>
    {
        private readonly ICountryWriteRepository _countryWriteRepository;
        private readonly ICountryReadRepository _countryReadRepository;
        public CountryWriteRepositoryTest(ITestOutputHelper output) : base(output)
        {
            var readLogger = new LoggerStubs<ICountryReadRepository>(output);
            _countryReadRepository = new CountryReadRepository(unitOfWorkWorldDb, readLogger);
            _countryWriteRepository = new CountryWriteRepository(
                unitOfWorkWorldDb,
                _countryReadRepository,
                logger
            );
        }

        [Fact]
        public async Task Add_Country_Test()
        {
            // Arrange
            string countryName = "sample add country test";
            string code = "TWT";
            Country country = DummyObject.GetCountryDummy(countryName: countryName, code: code);

            // Act
            var res = await _countryWriteRepository.AddCountry(country, default);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(countryName, res.Name);
        }

        [Fact]
        public async Task Update_Country_Test()
        {
            // Arrange
            string countryName = "sample Update country test";
            string code = "TOT";
            Country? country = DummyObject.GetCountryDummy(countryName: countryName, code: code);
            AddEntity(country!);
            country = _countryReadRepository.SelectAsync(
                x => x.Name == countryName,
                default)
                .Result
                .FirstOrDefault();
            country.Name = "updated country Name";

            // Act
            var resUpdate = await _countryWriteRepository.UpdateCountry(country, default);

            // Assert
            Assert.NotNull(country);
            Assert.NotNull(resUpdate);
            Assert.Equal(country.Name, resUpdate.Name);
        }

        [Fact]
        public async Task Delete_Country_Test()
        {
            // Arrange
            string countryName = "sample delete country test";
            string code = "TLT";
            Country? country = DummyObject.GetCountryDummy(countryName: countryName, code: code);
            AddEntity(country!);
            country = _countryReadRepository.SelectAsync(
                x => x.Name == countryName,
                default)
                .Result
                .FirstOrDefault();

            // Act
            var res = await _countryWriteRepository.DeleteCountry(country!, default);

            // Assert
            Assert.NotNull(country);
            Assert.NotNull(res);
            Assert.Equal(countryName, res.Name);
        }
        [Fact]
        public async Task Delete_Country_By_Code_Test()
        {
            // Arrange
            string countryName = "sample delete country by code test";
            string code = "TGT";
            Country country = DummyObject.GetCountryDummy(countryName: countryName, code: code);
            AddEntity(country);

            // Act
            var resDelete = await _countryWriteRepository.DeleteCountry(code, default);
            var select = await _countryReadRepository.SelectAsync(
                x => x.Code == code,
                default
            );
            
            // Assert
            Assert.True(resDelete);
            Assert.Empty(select);
        }
    }
}
