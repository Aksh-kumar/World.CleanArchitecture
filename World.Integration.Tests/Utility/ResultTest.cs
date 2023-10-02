using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Domain.Shared;

namespace World.Integration.Tests.Utility
{
    [Serializable]
    public class ResultTest
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure { get; set; }
        public Error? Error { get; set; }

        public virtual Result GetResult()
        {
            CheckErrorIsNull();
            return (IsSuccess) ? Result.Success(IsSuccess) : Result.Failure(Error!);
        }

        protected void CheckErrorIsNull()
        {
            if (Error == null)
            {
                throw new NullReferenceException("can't create result object with Error is null");
            }
        }
    }

    [Serializable]
    public class ResultTest<TValue> : ResultTest
    {
        public TValue? Value { get; set; }

        public override Result<TValue> GetResult()
        {
            CheckErrorIsNull();
            CheckValueIsNull();
            return (IsSuccess) ? Result.Success<TValue>(Value!) : Result.Failure<TValue>(Error!);
        }

        private void CheckValueIsNull()
        {
            if (Value == null)
            {
                throw new NullReferenceException("can't create result object with Value is null");
            }
        }
    }
    

}
