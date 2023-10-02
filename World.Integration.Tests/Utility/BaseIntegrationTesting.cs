using Docker.DotNet.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using World.Domain.Shared;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace World.Integration.Tests.Utility
{
    public abstract class BaseIntegrationTesting : IClassFixture<IntegrationTestWebAppFactory>
    {
        protected readonly IServiceScope _scope;
        protected readonly ISender sender;
        protected readonly HttpClient client;

        public BaseIntegrationTesting(IntegrationTestWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();
            client = factory.CreateClient();
            client.BaseAddress = new Uri("http://localhost");
            sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        }

        protected async Task<TResponse?> HttpResponseBody<TResponse>(HttpResponseMessage? response) where TResponse : class
        {
            var jsonString = await response?.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var deserialized = JsonSerializer.Deserialize<TResponse>(jsonString, options!);
            return deserialized;
        }
    }
}
