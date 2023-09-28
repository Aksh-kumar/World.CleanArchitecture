using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts.Messaging;
using World.Application.ResponseDTO.Country;

namespace World.Application.Country.Query
{
    public sealed record GetCountryByNameQuery(string Name) : IQuery<GetCountryResponse>
    {
    }
}
