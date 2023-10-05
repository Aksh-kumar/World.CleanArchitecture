using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace World.Domain.Contract.BaseRepository;

/// <summary>
/// Implement this interface to get data using linq expression
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface ISelectService<TEntity> where TEntity : class
{

    /// <summary>
    /// Get filterd data as per linq expression
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
}
