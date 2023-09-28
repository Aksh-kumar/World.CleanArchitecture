using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace World.Persistence.Contract.Base;

/// <summary>
/// Implement this interface while executing Inser or Update procedure/query on database which return number of records affected
/// </summary>
public interface IExcecuteQuery
{
    /// <summary>
    /// Executing Insert or Update procedure/query on database which return number of records affected
    /// </summary>
    /// <param name="query">procedure name</param>
    /// <returns></returns>
    Task<int> ExcecuteQueryAsync(string query);
}
