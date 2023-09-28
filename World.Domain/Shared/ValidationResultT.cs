using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Domain.Shared
{
    public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
    {
        private ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidationError)
        {
            this.Errors = errors;
        }

        public Error[] Errors { get; init; }

        public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
    }
}
