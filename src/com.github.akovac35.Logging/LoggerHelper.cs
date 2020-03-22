// Author: Aleksander Kovač

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace com.github.akovac35.Logging
{
    public static class LoggerHelper<T>
    {
        /// <summary>
        /// This method is not thread safe, do not store the reference. The logging system must first be initialized
        /// before the logger can be used.
        /// </summary>
        public static ILogger Logger
        {
            get
            {
                if (_logger == null) _logger = LoggerFactoryProvider.LoggerFactory.CreateLogger<T>();
                return _logger;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _logger = value;
            }
        }

        private static volatile ILogger _logger;

        public static void Here(Action<ILogger> logAction, [CallerMemberName] string callerMemberName = "unknown", [CallerFilePath] string callerFilePath = "unknown", [CallerLineNumber] int callerLineNumber = -1)
        {
            using (Logger.BeginScope(
               new[] { new KeyValuePair<string, object>(Constants.CallerMemberName, callerMemberName),
                        new KeyValuePair<string, object>(Constants.CallerFilePath, callerFilePath),
                        new KeyValuePair<string, object>(Constants.CallerLineNumber, callerLineNumber)})
                )
            {
                try
                {
                    logAction(Logger);
                }
                catch (Exception ex)
                {
                    Logger.LogTrace(ex, ex.Message);
                }
            }
        }
    }
}
