using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using World.Application.Country.Command;
using World.Persistence.DBContext;

namespace World.Integration.Tests
{
    public class CountryIntegrationTest : BaseIntegrationTesting
    {
        public CountryIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
            
        }

        [Fact]
        public async Task Create_should_Add_NewCountryToDatabase()
        {
            // Arrange
            string countryName = "sample_country";
            CreateCountryCommand request = new(HeadOfState: "sample head of state",
                                            Capital: 10,
                                            Name: countryName,
                                            SurfaceArea: 123.34m,
                                            IndepYear: 1993,
                                            Population: 8982182,
                                            LifeExpectancy: 78.4m,
                                            Gnp: 12.3m,
                                            Gnpold: 3232.2m);
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
           );
            // act
            var response  =  await client.PostAsync("/api/v2/country/add", jsonContent);

            // assert
            var getResponse = await client.GetAsync($"/api/v2/country/get?name={countryName}");
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
