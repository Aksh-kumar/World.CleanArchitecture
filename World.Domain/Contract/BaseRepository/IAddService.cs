namespace World.Domain.Contract.BaseRepository;

/// <summary>
/// This interface need to be implemented by repositoris 
/// which wants to add new record in entity
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IAddService<TEntity> where TEntity : class
{
    /// <summary>
    /// Add new record in to entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    /// <summary>
    /// Add List of records into entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddRangeAsync(List<TEntity> entity, CancellationToken cancellationToken);
}