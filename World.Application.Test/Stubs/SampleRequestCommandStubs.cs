using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts.Messaging;
using World.Domain.Shared;

namespace World.Unit.Test.Stubs
{
    public class SampleResponse
    {
        public string? Name { get; set; }
    }


    public class SampleRequestCommandStubs : ICommand<SampleResponse>
    {
        public string? Name { get; set; }
        public SampleRequestCommandStubs(string? name)
        {
            Name = name;
        }
    }

    public class SampleRequestCommandValidatorStubs :
        AbstractValidator<SampleRequestCommandStubs>, IValidator<SampleRequestCommandStubs>
    {
        public SampleRequestCommandValidatorStubs()
        {
            RuleFor(x => x.Name)
                .Must(x => !string.IsNullOrEmpty(x))
                .Must(x => x.Trim().Length != 0);
        }
    }
}
