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
    public static class ILoggerExtensions
    {
        public static void Here(this ILogger logger, Action<ILogger> logAction, [CallerMemberName] string callerMemberName = "unknown", [CallerFilePath] string callerFilePath = "unknown", [CallerLineNumber] int callerLineNumber = -1)
        {
            if (LoggerLibraryConfiguration.ShouldHerePassInvocationContextToLoggerScope)
            {
                using (logger.BeginScope(
               new[] { new KeyValuePair<string, object>(Constants.CallerMemberName, callerMemberName),
                        new KeyValuePair<string, object>(Constants.CallerFilePath, callerFilePath),
                        new KeyValuePair<string, object>(Constants.CallerLineNumber, callerLineNumber)})
                )
                {
                    try
                    {
                        logAction(logger);
                    }
                    catch (Exception ex)
                    {
                        logger.LogTrace(ex, "Here");
                    }
                }
            }
            else
            {
                try
                {
                    logAction(logger);
                }
                catch (Exception ex)
                {
                    logger.LogTrace(ex, "Here");
                }
            }
        }


        public static void Entering(this ILogger logger, params object?[] args)
        {
            try
            {
                if (logger.IsEnabled(LogLevel.Trace))
                {
                    // new object[] { args } is required to log all args items and not just the first one
                    // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
                    logger.LogTrace("Entering: {@0}", new object?[] { args });
                }
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex, "Entering");
            }
        }

        public static void EnteringSimpleFormat(this ILogger logger, params object?[] args)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                // new object[] { args } is required to log all args items and not just the first one
                // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
                logger.LogTrace("Entering: {0}", new object?[] { args });
            }
        }

        public static void Entering(this ILogger logger)
        {
            logger.LogTrace("Entering");
        }

        public static void Entering(this ILogger logger, LogLevel level, params object?[] args)
        {
            try
            {
                if (logger.IsEnabled(level))
                {
                    // new object[] { args } is required to log all args items and not just the first one
                    // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
                    logger.Log(level, "Entering: {@0}", new object?[] { args });
                }
            }
            catch (Exception ex)
            {
                logger.Log(level, ex, "Entering");
            }
        }

        public static void EnteringSimpleFormat(this ILogger logger, LogLevel level, params object?[] args)
        {
            if (logger.IsEnabled(level))
            {
                // new object[] { args } is required to log all args items and not just the first one
                // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
                logger.Log(level, "Entering: {0}", new object?[] { args });
            }
        }

        public static void Exiting(this ILogger logger, params object?[] args)
        {
            try
            {
                if (logger.IsEnabled(LogLevel.Trace))
                {
                    // new object[] { args } is required to log all args items and not just the first one
                    // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
                    logger.LogTrace("Exiting: {@0}", new object?[] { args });
                }
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex, "Exiting");
            }
        }

        public static void ExitingSimpleFormat(this ILogger logger, params object?[] args)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                // new object[] { args } is required to log all args items and not just the first one
                // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
                logger.LogTrace("Exiting: {0}", new object?[] { args });
            }
        }

        public static void Exiting(this ILogger logger)
        {
            logger.LogTrace("Exiting");
        }

        public static void Exiting(this ILogger logger, LogLevel level, params object?[] args)
        {
            try
            {
                if (logger.IsEnabled(level))
                {
                    // new object[] { args } is required to log all args items and not just the first one
                    // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
                    logger.Log(level, "Exiting: {@0}", new object?[] { args });
                }
            }
            catch (Exception ex)
            {
                logger.Log(level, ex, "Exiting");
            }
        }

        public static void ExitingSimpleFormat(this ILogger logger, LogLevel level, params object?[] args)
        {
            if (logger.IsEnabled(level))
            {
                // new object[] { args } is required to log all args items and not just the first one
                // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
                logger.Log(level, "Exiting: {0}", new object?[] { args });
            }
        }

        public static bool IsEnteringExitingEnabled(this ILogger logger)
        {
            return logger.IsEnabled(LogLevel.Trace);
        }
    }
}
