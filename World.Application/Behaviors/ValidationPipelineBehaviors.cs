using FluentValidation;
using MediatR;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Contracts.Messaging;
using World.Application.Contracts.pipelines;
using World.Application.Exception;
using World.Domain.Shared;

namespace World.Application.Behaviors
{
    public class ValidationPipelineBehaviors<TRequest, TResponse> 
        : IValidationPilelineBehavior<TRequest, TResponse>
        where TRequest : ICommandBase
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationPipelineBehaviors<TRequest, TResponse>> _logger;

        public ValidationPipelineBehaviors(
            IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationPipelineBehaviors<TRequest, TResponse>> logger
        )
        {
            _logger = logger;
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"checking If any Validation eror is there");
            if (!_validators.Any())
            {
                return await next();
            }
            _logger.LogInformation($"One or More validation error is found");
            var validationFailure = await Task.WhenAll(_validators
                .Select(validator => validator.ValidateAsync(request))
            );

            var errors = validationFailure
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .Select(validationFailure => new ValidationError(
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage)
                )
                .Distinct()
                .ToList();
            _logger.LogInformation($"validation error Error found Throwing exception");
            if (errors.Any())
            {
                throw new World.Application.Exception.ValidationException(errors);
            }
            return await next();
        }
        private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
        {
            if(typeof(TResult) == typeof(Result))
            {
                return (ValidationResult.WithErrors(errors) as TResult)!;
            }
            object validationResult = typeof(ValidationResult<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(Result).GenericTypeArguments[0])
                .GetMethod(nameof(ValidationResult.WithErrors))!
                .Invoke(null, new object?[] { errors })!;
            return (TResult)(validationResult);
        }
    }

    
}
