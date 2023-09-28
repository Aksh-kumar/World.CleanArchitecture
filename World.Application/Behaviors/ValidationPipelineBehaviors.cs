using FluentValidation;
using MediatR;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
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

        public ValidationPipelineBehaviors(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

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

            if(errors.Any())
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
