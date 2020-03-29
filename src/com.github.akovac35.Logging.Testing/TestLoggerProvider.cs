﻿// Author: Microsoft, Aleksander Kovač

using Microsoft.Extensions.Logging;

namespace com.github.akovac35.Logging.Testing
{
    public class TestLoggerProvider : ILoggerProvider
    {
        private readonly ITestSink _sink;

        public TestLoggerProvider(ITestSink sink)
        {
            _sink = sink;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TestLogger(categoryName, _sink, enabled: true);
        }

        public void Dispose()
        {
        }
    }
}