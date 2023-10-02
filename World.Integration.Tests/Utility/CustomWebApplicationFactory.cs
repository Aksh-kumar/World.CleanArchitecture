using Asp.Versioning;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Data.Common;
using System.Xml.Linq;
using Testcontainers.MsSql;
using World.Persistence.DBContext;
using Testcontainers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Docker.DotNet.Models;
using System;

namespace World.Integration.Tests.Utility
{
    public abstract class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram : class
    {
        #region Private variables
        private const string SQL_SERVER_IMAGE = "mcr.microsoft.com/mssql/server:2022-latest";
        private readonly MsSqlContainer _container;
        private const string HOST = "127.0.0.1";
        private const string PASSWORD = "password@1234";
        private const int PORT = 1433;
        private const string DATABASE = "worldDb";
        private const string USER_ID = "sa";
        #endregion
        #region constructor
        public CustomWebApplicationFactory()
        {
            _container = new MsSqlBuilder()
                .WithImage(SQL_SERVER_IMAGE)
                .WithPassword(PASSWORD)
                .WithCleanUp(true)
                .Build();
        }
        #endregion
        #region public methods
        public async Task InitializeAsync()
        {
            await _container.StartAsync();
            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<WorldDBContext>();
            try
            {
                dbContext.Database.EnsureCreated();
            }
            catch (Exception)
            {
                throw;
            }
            await dbContext.Database.MigrateAsync();

        }
        public new async Task DisposeAsync() => await _container.DisposeAsync();
        #endregion
        #region protected methods
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            _ = builder.ConfigureServices(services =>
            {

                var dbContextDescriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<WorldDBContext>)
                );

                if (dbContextDescriptor != null)
                {
                    _ = services.Remove(dbContextDescriptor!);
                }

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                if (dbConnectionDescriptor != null)
                {
                    _ = services.Remove(dbConnectionDescriptor!);
                }

                services.AddDbContext<WorldDBContext>((container, options) =>
                {
                    string connectionString = GetConnectionString();
                    options.UseSqlServer(connectionString);
                });
            });

            builder.UseEnvironment("Development");
        }
        #endregion
        #region private methods
        private string GetConnectionString()
        {
            return $"Server={HOST},{_container.GetMappedPublicPort(PORT)};Database={DATABASE};User Id={USER_ID};Password={PASSWORD};TrustServerCertificate=True";
        }
        #endregion
    }
}