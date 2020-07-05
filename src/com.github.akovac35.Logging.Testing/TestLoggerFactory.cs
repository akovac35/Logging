// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač
//   Microsoft

using Microsoft.Extensions.Logging;
using System;

namespace com.github.akovac35.Logging.Testing
{
    public class TestLoggerFactory : ILoggerFactory
    {
        protected ITestSink _sink;

        public TestLoggerFactory(ITestSink sink)
        {
            _sink = sink ?? throw new ArgumentNullException(nameof(sink));
        }

        public virtual ILogger CreateLogger(string name)
        {
            return NewLogger(name ?? throw new ArgumentNullException(nameof(name)));
        }

        protected virtual ILogger NewLogger(string name)
        {
            return new TestLogger(name, _sink);
        }

        public virtual void AddProvider(ILoggerProvider provider)
        {
        }

        public virtual void Dispose()
        {
        }
    }
}
