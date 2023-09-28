using World.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using World.Application.Contracts.pipelines;

namespace World.Application.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse>
    : ILoggingPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {@RequestName} {@DateTimeUTC}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();

        _logger.LogInformation("Completed request {@RequestName} {@DateTimeUTC}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        return result;
    }
}
