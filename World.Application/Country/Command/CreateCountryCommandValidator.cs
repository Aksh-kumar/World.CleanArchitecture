using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Application.Country.Command
{
    internal class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(x => x.IndepYear)
             .Must(x => x!.Value <= (short)DateTime.Now.Year);

        }
    }
}
