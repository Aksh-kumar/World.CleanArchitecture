using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Domain.Shared
{
    public sealed class ValidationResult : Result, IValidationResult
    {

        private ValidationResult(Error[] errors) : base(false, IValidationResult.ValidationError)
        {
            this.Errors = errors;
        }

        public Error[] Errors { get; init; }

        public static ValidationResult WithErrors(Error[] errors) => new(errors);
    }
}
