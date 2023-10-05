using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Shared;

namespace World.Domain.Contract.BaseRepository;

public interface IUnitOfWork : IDisposable
{
    DatabaseType DatabaseType { get; }
    DbContext DbContext { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
