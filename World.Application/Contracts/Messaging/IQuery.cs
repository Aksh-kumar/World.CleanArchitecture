
using MediatR;
using World.Domain.Shared;

namespace World.Application.Contracts.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
