using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace World.Domain.Contract.BaseRepository;

/// <summary>
/// This interface need to be implemented by repositoris 
/// which wants to modify record details in entity
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IUpdateService<TEntity> where TEntity : class
{
    /// <summary>
    /// Update Single Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity?> Update(TEntity entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// Update record in to entity
    /// </summary>
    /// <param name="entity">Database/DBContext entity</param>
    /// <returns></returns>
    Task UpdateAsync(TEntity entity);
    /// <summary>
    /// Upadte Multiple Entity
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    Task UpdateAllAsync(List<TEntity> list);
}
