using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using World.Domain.Contract.BaseRepository;
using World.Domain.DomainEntity.World;


namespace World.Persistence.Repository.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable
        where TEntity : class
    {
        protected readonly IQueryable<TEntity> _iqueryableObject;
        protected readonly IUnitOfWork _unitOfWork;
        private bool _disposed;
        #region constructor
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _disposed = false;
            _unitOfWork = unitOfWork;
            _iqueryableObject = _unitOfWork.DbContext.Set<TEntity>();
        }
        #endregion
        #region public Methods
        public IUnitOfWork UnitOfWorkPropery => _unitOfWork;
        public async Task<IEnumerable<TEntity>> SelectAllAsync()
        {
            return await _unitOfWork.DbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IList<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _unitOfWork.DbContext.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
        }
        public async Task<TEntity?> Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await AddAsync(entity);
                int res = await _unitOfWork.SaveChangesAsync(cancellationToken);
                return res == 1 ? entity : null;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            
        }
        public async Task<TEntity?> Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entity);
            int res = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return res == 1 ? entity : null;
        }
        public async Task<TEntity?> Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entity);
            int res = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return res == 1 ? entity : null;
        }
        #region Database Operation
        /// <summary>
        /// Add new record in entity provided by repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity entity)
        {
            await Task.Run(() => _unitOfWork.DbContext.Set<TEntity>().AddAsync(entity));
        }
        /// <summary>
        ///  Add multiple record in entity provided by repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task AddRangeAsync(List<TEntity> entity)
        {
            await Task.Run(() => _unitOfWork.DbContext.Set<TEntity>().AddRangeAsync(entity));
        }
        /// <summary>
        /// Modify single record from entity provided by repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _unitOfWork.DbContext.Set<TEntity>().Update(entity));
        }
        /// <summary>
        ///  Modify Multiple record from entity provided by repository
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public virtual async Task UpdateAllAsync(List<TEntity> entityList)
        {
            await Task.Run(() => _unitOfWork.DbContext.Set<TEntity>().UpdateRange(entityList));
        }

        /// <summary>
        /// Delete single record from entity provided by repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _unitOfWork.DbContext.Set<TEntity>().Remove(entity));
        }
        /// <summary>
        /// Delete range of records from entity provided by repository
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public virtual async Task DeleteRangeAsync(List<TEntity> entityList)
        {
            await Task.Run(() => _unitOfWork.DbContext.Set<TEntity>().RemoveRange(entityList));

        }
        #endregion
        #endregion

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