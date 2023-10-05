using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Behaviors;
using World.Application.Contracts.Messaging;
using World.Application.Exception;
using World.Domain.Shared;
using World.Unit.Test.Stubs;
using Xunit.Abstractions;

namespace World.Unit.Test.core.ApplicationTest.BehavioursTest
{
    public class ValidationPipelineBehaviorTest
    {
        private readonly ILogger<ValidationPipelineBehaviors<SampleRequestCommandStubs, Result>> _logger;
        private readonly List<IValidator<SampleRequestCommandStubs>> _validators;
        private readonly RequestHandlerDelegate<Result> _next;
        public ValidationPipelineBehaviorTest(ITestOutputHelper output)
        {
            _logger = new LoggerStubs<ValidationPipelineBehaviors<SampleRequestCommandStubs, Result>>(
                output
             );
            _validators = new()
            {
               new SampleRequestCommandValidatorStubs()
            };
            _next = () => Task.Run(() => Result.Success());
        }
        [Theory]
        [InlineData(null)]
        public async Task Should_not_create_recipe_when_name_is_null_Test(string? name)
        {
            // Arrange
            var sampleRequestCommand = new SampleRequestCommandStubs(name);
            var validationBehavior = new ValidationPipelineBehaviors<SampleRequestCommandStubs, Result>(
                _validators,
                _logger
            );

            // Act Assert
            await Assert.ThrowsAsync<NullReferenceException>(() =>
                validationBehavior.Handle(sampleRequestCommand, _next, default)
            );
        }
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public async Task Should_not_create_recipe_when_name_is_empty_Test(string? name)
        {
            // Arrange
            var sampleRequestCommand = new SampleRequestCommandStubs(name);
            var validationBehavior = new ValidationPipelineBehaviors<SampleRequestCommandStubs, Result>(
                _validators,
                _logger
            );

            // Act and Assert
            await Assert.ThrowsAsync<Application.Exception.ValidationException>(() =>
                validationBehavior.Handle(sampleRequestCommand, _next, default)
            );
        }
    }
}
