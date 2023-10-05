using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Contract.BaseRepository;
using World.Domain.Shared;
using World.Persistence.Common;
using World.Persistence.Contracts;
using World.Persistence.DBContext;

namespace World.Persistence.Repository.Base
{
    public class UnitOfWorkWorldDb : IUnitOfWorkWorldDb
    {
        private readonly DbContext _context;
        private bool _disposed;
        #region contructor
        public UnitOfWorkWorldDb(Func<DatabaseType, DbContext> resolver)
        {
            _context = resolver(DatabaseType.WORLD_DB);
            _disposed = false;
            DatabaseType = DatabaseType.WORLD_DB;
        }
        #endregion

        #region public
        public DatabaseType DatabaseType { get; init; }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public DbContext DbContext { get { return _context; } }
        #endregion
        #region dispose
        protected void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                if (_context != null) _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}