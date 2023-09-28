using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Application.Exception
{
    internal class ValidationException : System.Exception
    {
        public ValidationException(IReadOnlyCollection<ValidationError> error): base("Validation failed")
        {
            ValidationErrors = error;
        }

        public IReadOnlyCollection<ValidationError> ValidationErrors { get; init; }
    }

    public record ValidationError(string PropertyName, string EerrorMessage);
}
