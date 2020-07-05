// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace com.github.akovac35.Logging
{
    public static class LoggerFactoryProvider
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        static LoggerFactoryProvider()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            SetDefaultLoggerFactory();
        }

        private static ILoggerFactory _loggerFactory;

        public static ILoggerFactory LoggerFactory
        {
            get
            {
                return _loggerFactory;
            }
            set
            {
                _loggerFactory = value ?? throw new ArgumentNullException(nameof(LoggerFactory));
            }
        }

        public static void SetDefaultLoggerFactory()
        {
            _loggerFactory = NullLoggerFactory.Instance;
        }
    }
}