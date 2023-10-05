using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Shared;
using World.Persistence.DBContext;

namespace World.Unit.Test.Helper
{
    public static class DBResolver
    {
        public static DbContext DatabaseResolver(DatabaseType databaseType)
        {
            switch (databaseType) {
                case DatabaseType.WORLD_DB:
                    var optionsBuilder = new DbContextOptionsBuilder<WorldDBContext>();
                    optionsBuilder.UseInMemoryDatabase("WorldDb");
                    WorldDBContext dbContext = new(optionsBuilder.Options);
                    return dbContext;
                default:
                    throw new NotImplementedException("given database type DbContext not implemented");

            }
        }
    }
}
