using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace World.Domain.Contract.BaseRepository;

/// <summary>
/// Implement this interface to get all data
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface ISelectAllService<TEntity> where TEntity : class
{
    /// <summary>
    ///  Get filterd data as per linq expression
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<TEntity>> SelectAllAsync(CancellationToken cancellationToken);
}
