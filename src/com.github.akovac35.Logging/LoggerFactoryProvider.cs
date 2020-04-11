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
        static LoggerFactoryProvider()
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
                if (value == null) throw new ArgumentNullException(nameof(value));
                _loggerFactory = value;
            }
        }

        public static void SetDefaultLoggerFactory()
        {
            _loggerFactory = new NullLoggerFactory();
        }
    }
}
