// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace com.github.akovac35.Logging
{
    /// <summary>
    /// Use logger helper for logging application startup events or for limited logging inside types without having to inject a logger instance. Never use it inside static constructors or use its reference for initialization of variables.
    /// </summary>
    /// <typeparam name="T">Logger type.</typeparam>
    public static class LoggerHelper<T>
    {
        public static ILogger Logger
        {
            get
            {
                // Reset logger if logger factory which created it is no longer current. We don't care for thread safety
                // because when steady state is reached, everything should be consistent
                ILoggerFactory lfCurrent = LoggerFactoryProvider.LoggerFactory;
                ILoggerFactory lf = null;
                _loggerFactoryWhichCreatedLogger.TryGetTarget(out lf);
                if (lf != lfCurrent || _logger == null)
                {
                    _logger = lfCurrent.CreateLogger<T>();
                    _loggerFactoryWhichCreatedLogger.SetTarget(lfCurrent);
                }

                return _logger;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _logger = value;
            }
        }

        private static object _getLoggerLock = new object();

        private static volatile ILogger _logger;

        private static WeakReference<ILoggerFactory> _loggerFactoryWhichCreatedLogger = new WeakReference<ILoggerFactory>(null);

        public static void Here(Action<ILogger> logAction, [CallerMemberName] string callerMemberName = "unknown", [CallerFilePath] string callerFilePath = "unknown", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (LoggerLibraryConfiguration.ShouldHerePassInvocationContextToLoggerScope)
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
            else
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
