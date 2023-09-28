using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace World.Persistence.Contract.Base;

/// <summary>
/// Implement this interface while executing procedure directly on database entity
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IExecuteEntity<TEntity> where TEntity : class
{
    /// <summary>
    /// Execute procedure directly on database entity
    /// </summary>
    /// <param name="query">procedure name</param>
    /// <param name="param"> Should be sql parameters</param>
    /// <returns></returns>
    Task<List<TEntity>> ExecuteEntityAsync<T>(string procName, params T[] sqlParam) where T : class;
}
