namespace World.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        return serviceCollection;
    }
}