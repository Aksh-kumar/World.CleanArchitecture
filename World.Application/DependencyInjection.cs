using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using World.Application.WeatherForecast.Services;
using World.Application.Contracts.Services;
using World.Application.Country.Services;
using World.Application.Behaviors;
using World.Application.Contracts.pipelines;

namespace World.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        serviceCollection.AddAutoMapper(assembly);

        serviceCollection = serviceCollection.AddMediatR((configuration) =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        /******************** Fluent Validatoin Injection ***************/

        serviceCollection = serviceCollection.AddValidatorsFromAssembly(
            assembly,
            includeInternalTypes: true
        );

        /******************************************************************/

        serviceCollection.AddScoped(typeof(ILoggingPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        serviceCollection.AddScoped(typeof(IValidationPilelineBehavior<,>), typeof(ValidationPipelineBehaviors<,>));
        
        /***********************  Services Injection ***********************************************/

        serviceCollection.AddScoped<IWeatherService, WeatherForecastService>();
        serviceCollection.AddScoped<ICountryService, CountryService>();

        /*******************************************************************************************/

        return serviceCollection;
    }
}