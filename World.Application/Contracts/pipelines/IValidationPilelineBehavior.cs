using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using World.Application.Contracts.Messaging;
using World.Domain.Shared;

namespace World.Application.Contracts.pipelines
{
    public interface IValidationPilelineBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommandBase
        where TResponse : Result
    {
    }
}
