// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using NLog.Extensions.Logging;

namespace com.github.akovac35.Logging.NLog
{
    public static class NLogHelper
    {
        public static void CreateLogger()
        {
            global::NLog.LogManager.LoadConfiguration("NLog.config");
        }

        public static void CreateLogger(string xmlConfigFileName)
        {
            global::NLog.LogManager.LoadConfiguration(xmlConfigFileName);
        }

        public static void CloseAndFlushLogger()
        {
            global::NLog.LogManager.Shutdown();
        }

        public static Microsoft.Extensions.Logging.ILoggerFactory CreateLoggerFactory(NLogProviderOptions options = null)
        {
            return new NLogLoggerFactory(options ?? new NLogProviderOptions());
        }
    }
}
