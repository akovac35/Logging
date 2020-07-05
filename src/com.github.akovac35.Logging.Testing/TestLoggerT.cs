// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač
//   Microsoft

using Microsoft.Extensions.Logging;
using System;

namespace com.github.akovac35.Logging.Testing
{
    public class TestLogger<T> : ILogger<T>
    {
        protected ILogger _logger;

        public TestLogger(TestLoggerFactory factory)
        {
            _logger = (factory ?? throw new ArgumentNullException(nameof(factory))).CreateLogger<T>();
        }

        public virtual IDisposable BeginScope<TState>(TState state)
        {
            return _logger.BeginScope(state);
        }

        public virtual bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        public virtual void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}
