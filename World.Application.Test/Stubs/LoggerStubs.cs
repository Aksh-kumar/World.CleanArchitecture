using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace World.Unit.Test.Stubs;

public class LoggerStubs<T> : ILogger<T>, IDisposable
{
    private readonly ITestOutputHelper _output;
    public LoggerStubs(ITestOutputHelper output)
    {
        _output = output;
    }
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return this;
    }

    public void Dispose()
    {
        // do nothing
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        _output.WriteLine(state.ToString());
    }
}
