using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.ResponseDTO.Country;
using World.Domain.DomainEntity.World;
using World.Domain.Shared;
using World.Integration.Tests.Utility;
using World.Persistence.DBContext;
using World.Unit.Test.Helper;

namespace World.Integration.Tests.Controllers.V1
{
    public class CountryIntegrationTest : BaseIntegrationTesting
    {
        private const string GET_COUNTRY_V1_URL = "/api/v1/country/get?name=";
        private const string SAMPLE_GET_COUNTRY_NAME = "GET_REQUEST_V1_COUNTRY";
        public CountryIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
            using var dbContext = _scope.ServiceProvider.GetRequiredService<WorldDBContext>();
            dbContext.Set<Country>().Add(DummyObject.GetCountryDummy(SAMPLE_GET_COUNTRY_NAME, "GWT"));
            int _ = dbContext.SaveChanges();
        }

        [Fact]
        public async Task Get_should_Return_The_CountryObject()
        {
            // Arrange
            string countryName = SAMPLE_GET_COUNTRY_NAME;

            // Act
            var getResponse = await client.GetAsync($"{GET_COUNTRY_V1_URL}{countryName}");
            GetCountryResponse? getResult = await HttpResponseBody<GetCountryResponse>(getResponse);

            // Assert
            Assert.True(getResponse.IsSuccessStatusCode);
            Assert.Equal(countryName, getResult.Name);
        }
    }
}
