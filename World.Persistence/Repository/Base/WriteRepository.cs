using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Contract.BaseRepository;

namespace World.Persistence.Repository.Base
{
    public abstract class WriteRepository<TEntity> : BaseRepository<TEntity>, IWriteRepository<TEntity>, IDisposable where TEntity : class
    {
        
        #region constructor
        public WriteRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }
        #endregion
        #region Public Methods
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await Task.Run(() => entities.AddAsync(entity, cancellationToken));
        }
        public virtual async Task AddRangeAsync(List<TEntity> entity, CancellationToken cancellationToken)
        {
            await Task.Run(() => entities.AddRangeAsync(entity, cancellationToken));
        }
        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            StopTracking();
            await Task.Run(() => entities.Update(entity), cancellationToken);
        }
        public virtual async Task UpdateAllAsync(List<TEntity> entityList, CancellationToken cancellationToken)
        {
            StopTracking();
            await Task.Run(() => entities.UpdateRange(entityList), cancellationToken);
        }
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            StopTracking();
            await Task.Run(() => entities.Remove(entity), cancellationToken);
        }
        public virtual async Task DeleteRangeAsync(List<TEntity> entityList, CancellationToken cancellationToken)
        {
            StopTracking();
            await Task.Run(() => entities.RemoveRange(entityList), cancellationToken);

        }
        #endregion
    }
}
