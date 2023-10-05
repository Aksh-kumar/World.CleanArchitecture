using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Contract.BaseRepository;

namespace World.Persistence.Repository.Base
{
    public abstract class BaseRepository<TEntity>  : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IQueryable<TEntity> _iqueryableObject;
        protected readonly IUnitOfWork _unitOfWork;
        private bool _disposed;
        protected readonly DbSet<TEntity> entities;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _disposed = false;
            _unitOfWork = unitOfWork;
            entities = _unitOfWork.DbContext.Set<TEntity>();
            _iqueryableObject = entities.AsNoTracking();
        }

        public IUnitOfWork UnitOfWorkPropery => _unitOfWork;

        protected void StopTracking()
        {
            _unitOfWork.DbContext.ChangeTracker.Clear();
        }
        #region Dispose
        /// <summary>
        /// Method to dispose.
        /// </summary>
        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _unitOfWork.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposed = true;
        }
        #endregion
    }
}
