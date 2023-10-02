using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using World.Application.Country.Command;
using World.Application.ResponseDTO.Country;
using World.Domain.DomainEntity.World;
using World.Domain.Shared;
using World.Integration.Tests.Base;
using World.Integration.Tests.Utility;
using World.Persistence.DBContext;
using World.Unit.Test.Dummy;

namespace World.Integration.Tests.Controllers.V2
{
    public class CountryIntegrationTest : BaseIntegrationTesting
    {
        private const string POST_COUNTRY_V2_URL = "/api/v2/country/add";
        private const string GET_COUNTRY_V2_URL = "/api/v2/country/get?name=";
        private const string SAMPLE_GET_COUNTRY_NAME = "GET_REQUEST_V2_COUNTRY";
        public CountryIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
            using var dbContext = _scope.ServiceProvider.GetRequiredService<WorldDBContext>();
            dbContext.Set<Country>().Add(DummyObject.GetCountryDummy(SAMPLE_GET_COUNTRY_NAME, "GST"));
            int _ = dbContext.SaveChanges();
        }
        [Fact]
        public async Task Get_should_Return_The_CountryObject()
        {
            // Arrange
            string countryName = SAMPLE_GET_COUNTRY_NAME;

            // Act
            var getResponse = await client.GetAsync($"{GET_COUNTRY_V2_URL}{countryName}");
            ResultTest<GetCountryResponse>? getBody = await HttpResponseBody<ResultTest<GetCountryResponse>>(getResponse);
            Result<GetCountryResponse> getResult = getBody.GetResult();

            // Assert
            Assert.True(getResponse.IsSuccessStatusCode);
            Assert.True(getBody.IsSuccess);
            Assert.Equal(countryName, getResult.Value.Name);
        }
        [Fact]
        public async Task Post_should_Add_NewCountryToDatabase()
        {
            // Arrange
            string countryName = "sample_country";
            CreateCountryCommand request = DummyObject.GetCreateCountryCommandDummy(countryName, "PST");
            StringContent jsonContent = new(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            // act
            var postResponse = await client.PostAsync(POST_COUNTRY_V2_URL, jsonContent);
            ResultTest<GetCountryResponse>? postBody = await HttpResponseBody<ResultTest<GetCountryResponse>>(postResponse);
            Result<GetCountryResponse> postResult = postBody.GetResult();

            // assert
            Assert.True(postResponse.IsSuccessStatusCode);
            Assert.True(postBody.IsSuccess);
            Assert.Equal(request.Name, postResult.Value.Name);
        }
    }
}
