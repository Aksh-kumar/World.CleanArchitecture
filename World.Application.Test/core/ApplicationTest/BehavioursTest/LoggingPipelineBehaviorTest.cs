using Azure;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World.Application.Behaviors;
using World.Application.Country.Command;
using World.Domain.Shared;
using World.Unit.Test.Stubs;
using Xunit.Abstractions;

namespace World.Unit.Test.core.ApplicationTest.BehavioursTest
{
    public class LoggingPipelineBehaviorTest
    {
        private readonly ILogger<LoggingPipelineBehavior<BehaviorRequestStub<Result>, Result>> _loggerMock;
        private readonly BehaviorRequestStub<Result> behaviorRequestStub;
        public LoggingPipelineBehaviorTest(ITestOutputHelper output)
        {
            _loggerMock = new LoggerStubs<LoggingPipelineBehavior<BehaviorRequestStub<Result>, Result>>(output);
            behaviorRequestStub = new();

        }

        [Fact]
        public async Task Logging_Pipeline_Behaviour_Handle_Test()
        {
            // Arrange
            RequestHandlerDelegate<Result> next = () => Task.Run(() => Result.Success());
            CancellationToken cancellationToken = new();
            LoggingPipelineBehavior<BehaviorRequestStub<Result>, Result> loggingPipelineBehavior = new(
                _loggerMock
                );

            // Act
            var result = await loggingPipelineBehavior.Handle(
                behaviorRequestStub, 
                next,
                cancellationToken
            );

            // Assert
            Assert.True(result.IsSuccess);

        }
    }
}
