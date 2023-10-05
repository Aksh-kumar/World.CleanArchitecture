using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace World.Domain.Contract.BaseRepository;

/// <summary>
/// This interface need to be implemented by repositoris 
/// which wants to delete single record form entity
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IDeleteService<TEntity> where TEntity : class
{
    /// <summary>
    /// Delete record in to entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    /// <summary>
    /// Delete multiple record in to entity
    /// </summary>
    /// <param name="entityList"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteRangeAsync(List<TEntity> entityList, CancellationToken cancellationToken);
}
