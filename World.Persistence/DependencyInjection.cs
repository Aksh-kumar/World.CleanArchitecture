using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using World.Domain.Contract;
using World.Domain.Contract.BaseRepository;
using World.Persistence.Common;
using World.Persistence.Contracts;
using World.Persistence.DBContext;
using World.Persistence.Repository;
using World.Persistence.Repository.Base;

namespace World.Persistence;
// docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=7766844174Aksh@' -p 1433:1433 -v C:/Users/aksh1/Documents/sql_server_2022/data:/var/opt/mssql/data -v C:/Users/aksh1/Documents/sql_server_2022/log:/var/opt/mssql/log -v C:/Users/aksh1/Documents/sql_server_2022/secrets:/var/opt/mssql/secrets --name sql1 --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest
// select default project as World.Domain
// Scaffold-DbContext -Connection name=WorldDB -OutputDir DomainEntity/World/ -ContextDir ../World.Persistence/DBContext -Provider Microsoft.EntityFrameworkCore.SqlServer -Context WorldDBContext -DataAnnotations -Namespace World.Domain.DomainEntity.World -contextNamespace World.Persistence.DBContext -Project World.Domain -StartupProject World.WebApi -Force -NoOnConfiguring
public static class DependencyInjection
{
    public static IServiceCollection AddPersistant(this IServiceCollection serviceCollection, [NotNull] IConfiguration Configuration)
    {
        var assembly = PersistantAssambly.Instance;

        serviceCollection.AddDbContext<WorldDBContext>(Options =>
            Options.UseSqlServer(Configuration.GetConnectionString("WorldDB")
            ));

        // serviceCollection.AddScoped<DbContext, WorldDBContext>();
        serviceCollection = serviceCollection.AddScoped<Func<DatabaseType, DbContext>>(provider => key =>
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return key switch
            {
                DatabaseType.WORLD_DB => new WorldDBContext(provider.
                GetService<DbContextOptions<WorldDBContext>>()),

                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
#pragma warning restore CS8604 // Possible null reference argument.
            };
        });
        /*******************   Register Repository Injection **************************/

        serviceCollection.AddScoped<IUnitOfWorkWorldDb, UnitOfWorkWorldDb>();
        serviceCollection.AddScoped<ICityRepository, CityRepository>();
        serviceCollection.AddScoped<ICountryRepository, CountryRepository>();

        /*****************************************************************************/
        return serviceCollection;
    }
}