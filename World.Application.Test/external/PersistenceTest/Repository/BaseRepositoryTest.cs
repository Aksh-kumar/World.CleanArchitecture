using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Contract.Read;
using World.Domain.DomainEntity.World;
using World.Persistence.Contracts;
using World.Persistence.Repository.Base;
using World.Unit.Test.Helper;
using World.Unit.Test.Stubs;
using Xunit.Abstractions;

namespace World.Unit.Test.external.PersistenceTest.Repository
{
    public class BaseRepositoryTest<TEntity, TRepository> : IDisposable where TEntity : class
    {
        protected readonly IUnitOfWorkWorldDb unitOfWorkWorldDb;
        protected readonly DbSet<TEntity> entities;
        protected readonly LoggerStubs<TRepository> logger;
        public BaseRepositoryTest(ITestOutputHelper output)
        {
            logger = new LoggerStubs<TRepository>(output);
            unitOfWorkWorldDb  = new UnitOfWorkWorldDb(DBResolver.DatabaseResolver);
            entities = unitOfWorkWorldDb.DbContext.Set<TEntity>();
        }
       protected void AddEntity(TEntity entity)
        {
            entities.Add(entity);
            _ = unitOfWorkWorldDb.SaveChangesAsync().Result;
        }

        public void Dispose()
        {
            // dispose if any
        }

    }
}
