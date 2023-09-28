using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using v1 = World.Presentation.Controllers.v1;
using v2 = World.Presentation.Controllers.v2;
using Asp.Versioning;

namespace World.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection serviceCollection)
    {
        var assembly = PresentationAssembly.Instance;

        serviceCollection
            .AddApiVersioning(x =>
        {
            x.AssumeDefaultVersionWhenUnspecified = true;
            x.ReportApiVersions = true;
            ApiVersionReader.Combine(
            new HeaderApiVersionReader("x-api-version"),
            new UrlSegmentApiVersionReader(),
            new MediaTypeApiVersionReader("x-api-version")
            );
        }).
        AddMvc().
        AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        return serviceCollection;
    }
}
