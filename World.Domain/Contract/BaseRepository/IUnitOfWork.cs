using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace World.Domain.Contract.BaseRepository;

public interface IUnitOfWork : IDisposable
{
    DbContext DbContext { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
