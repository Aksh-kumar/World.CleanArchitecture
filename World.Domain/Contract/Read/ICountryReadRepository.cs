using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Contract.BaseRepository;
using World.Domain.DomainEntity.World;

namespace World.Domain.Contract.Read;

public interface ICountryReadRepository : IReadRepository<Country> , IDisposable
{
    /// <summary>
    /// Select Country By Name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<Country?> SelectCountryByName(string name, CancellationToken cancellationToken);
}
