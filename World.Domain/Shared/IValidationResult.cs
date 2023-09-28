using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Domain.Shared
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new("Validation Error",
            "A Validation Problem occured.");

        Error[] Errors { get; }
    }
}
