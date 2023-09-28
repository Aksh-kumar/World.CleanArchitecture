using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using World.Persistence.Common;

namespace World.Persistence.Contract.Base;

/// <summary>
/// Use this interface to execute stored procedures for complex data type
/// </summary>
/// <typeparam name="QueryEntity"></typeparam>
public interface IExecuterStoreProc<ComplexEntity> : IDisposable where ComplexEntity : class
{
    /// <summary>
    /// Execute stored procedures for complex data type
    /// </summary>
    /// <param name="query">Procedure Name</param>
    /// <param name="sqlParam"></param>
    /// <returns></returns>
    Task<List<ComplexEntity>> ExecuteProcedureAsync(string procName, IEnumerable<Parameters> param);

    Task<List<ComplexEntity>> ExecuteProcAsync(string procName, IEnumerable<Parameters>? param = null);

    /// <summary>
    /// Execute procedure using reader on databse to get record
    /// </summary>
    /// <param name="procName">Procedure Name</param>
    /// <returns></returns>
    Task<List<ComplexEntity>> ExecuteAsync(string procName);
}
