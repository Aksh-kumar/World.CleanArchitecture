using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using World.Domain.Contract.BaseRepository;
using World.Domain.DomainEntity.World;


namespace World.Persistence.Repository.Base
{
    public abstract class ReadRepository<TEntity> : BaseRepository<TEntity> , IReadRepository<TEntity>
        where TEntity : class
    {
        #region constructor
        public ReadRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion
        #region public Methods
        public async Task<List<TEntity>> SelectAllAsync(CancellationToken cancellationToken)
        {
            StopTracking();
            return await _iqueryableObject.ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            StopTracking();
            return await
                _iqueryableObject
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }
        #endregion
    }
}