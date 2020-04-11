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
                        logger.LogTrace(ex, ex.Message);
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
                    logger.LogTrace(ex, ex.Message);
                }
            }
        }

        public static void Entering(this ILogger logger, params object[] args)
        {
            try
            {
                logger.LogTrace("Entering: {@args}", args);
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex, "Entering");
            }
        }

        public static void EnteringSimpleFormat(this ILogger logger, params object[] args)
        {
            logger.LogTrace("Entering: {args}", args);
        }

        public static void Entering(this ILogger logger)
        {
            logger.LogTrace("Entering");
        }

        public static void Entering(this ILogger logger, LogLevel level, params object[] args)
        {
            try
            {
                logger.Log(level, "Entering: {@args}", args);
            }
            catch (Exception ex)
            {
                logger.Log(level, ex, "Entering");
            }
        }

        public static void EnteringSimpleFormat(this ILogger logger, LogLevel level, params object[] args)
        {
            logger.Log(level, "Entering: {args}", args);
        }

        public static void Exiting(this ILogger logger, params object[] args)
        {
            try
            {
                logger.LogTrace("Exiting: {@args}", args);
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex, "Exiting");
            }
        }

        public static void ExitingSimpleFormat(this ILogger logger, params object[] args)
        {
            logger.LogTrace("Exiting: {args}", args);
        }

        public static void Exiting(this ILogger logger)
        {
            logger.LogTrace("Exiting");
        }

        public static void Exiting(this ILogger logger, LogLevel level, params object[] args)
        {
            try
            {
                logger.Log(level, "Exiting: {@args}", args);
            }
            catch (Exception ex)
            {
                logger.Log(level, ex, "Exiting");
            }
        }

        public static void ExitingSimpleFormat(this ILogger logger, LogLevel level, params object[] args)
        {
            logger.Log(level, "Exiting: {args}", args);
        }

        public static bool IsEnteringExitingEnabled(this ILogger logger)
        {
            return logger.IsEnabled(LogLevel.Trace);
        }
    }
}
