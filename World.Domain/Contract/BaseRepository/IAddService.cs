namespace World.Domain.Contract.BaseRepository;

/// <summary>
/// This interface need to be implemented by repositoris 
/// which wants to add new record in entity
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IAddService<TEntity> where TEntity : class
{
    /// <summary>
    /// Add Single Entity to DB
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity?> Add(TEntity entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// Add new record in to entity
    /// </summary>
    /// <param name="entity">Database/DBContext entity</param>
    /// <returns></returns>
    Task AddAsync(TEntity entity);
    /// <summary>
    /// Add List of records into entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task AddRangeAsync(List<TEntity> entity);
}