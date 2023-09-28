using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Integration.Tests
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
    }
}
