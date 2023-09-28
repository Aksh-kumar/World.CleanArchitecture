using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.ResponseDTO.Country;

namespace World.Application.Contracts.Services
{
    public interface ICountryService : IDisposable
    {
        Task<GetCountryResponse?> GetCountryByName([NotNull] string name, CancellationToken cancellation);
    }
}
