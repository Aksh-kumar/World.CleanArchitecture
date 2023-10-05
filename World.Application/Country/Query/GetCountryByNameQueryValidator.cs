using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Country.Command;

namespace World.Application.Country.Query
{
    internal class GetCountryByNameQueryValidator : AbstractValidator<GetCountryByNameQuery>
    {
        public GetCountryByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .Must(x => !string.IsNullOrEmpty(x));
        }
    }
}
