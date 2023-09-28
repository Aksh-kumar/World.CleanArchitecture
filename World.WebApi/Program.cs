using World.Application;
using World.Infrastructure;
using Serilog; // require serialog and serialog.aspNetCore
using Microsoft.Extensions.DependencyInjection;
using World.Presentation;
using World.Persistence;
using MediatR;
using World.Application.Behaviors;
using Serilog.Settings.Configuration;
using Microsoft.OpenApi.Models;
using World.WebApi.config;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using World.WebApi.middleware;
using Microsoft.Extensions.Configuration;
using World.WebApi.filters;
using Asp.Versioning.ApiExplorer;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);



/*********************  Add Dependency Injection *****************/

builder.Services
    .AddApplication()
    .AddAuthorization()
    .AddInfrastructure()
    .AddPresentation()
    .AddPersistant(builder.Configuration);

/*****************************************************************/

/***************** Log Config *************************************/

var options = new ConfigurationReaderOptions(ApplicationAssembly.Instance, typeof(Program).Assembly);

// builder.Host.UseSerilog();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
    .ReadFrom
    .Configuration(context.Configuration, options);
});

/*******************  Authentication and Autherization *****************/

// Configure own cors policy
builder.Services.AddCors(o => o.AddPolicy("AllowMyOrigin", builder =>
{
    builder.WithOrigins(@"http://localhost:4201", "*");
    builder.SetIsOriginAllowed((host) => true);
    builder.AllowCredentials();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
}));

builder.Services.AddMvc(option =>
{
    option.EnableEndpointRouting = true;
    option.Filters.Add(new AuthorizationTokenFilter(builder.Configuration));
    // Add roles permission validator
    option.Filters.Add(new RolePermissionFilter());
    // Add Exception filter
    option.Filters.Add(new CustomExceptionFilter());
});

/************************************************************************/

/************************************************************************/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(o => o.OperationFilter<SwaggerDefaultValues>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerWithVersioning();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
        // options.RoutePrefix = ""; // swagger UI at the root index.html
    });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseMiddleware<AuthenticationMiddleware>();

app.UseMiddleware<AuthorizationMiddleware>();

app.UseMiddleware(typeof(ErrorHandlingMiddleware));

app.UseRouting();

app.UseCors("AllowMyOrigin");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

public partial class Program { }