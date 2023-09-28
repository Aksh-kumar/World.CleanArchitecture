using MediatR;
using World.Domain.Shared;

namespace World.Application.Contracts.Messaging;

public interface ICommandBase
{

}
public interface ICommand : IRequest<Result>, ICommandBase
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase
    where TResponse : class
{

}

