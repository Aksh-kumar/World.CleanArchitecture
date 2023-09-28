
using MediatR;
using World.Domain.Shared;

namespace World.Application.Contracts.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : class
{
}
