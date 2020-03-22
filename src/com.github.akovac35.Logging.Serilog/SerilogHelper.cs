// Author: Aleksander Kovač

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System;

namespace com.github.akovac35.Logging.Serilog
{
    public static class SerilogHelper
    {
        public static void CloseAndFlushLogger()
        {
            Log.CloseAndFlush();
        }

        public static void CreateLogger()
        {
            CreateLogger(configure =>
            {
                configure.AddJsonFile("serilog.json", optional: false, reloadOnChange: true);
            });
        }

        public static void CreateLogger(Action<IConfigurationBuilder> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            UpdateLogger(configure);
        }

        public static void UpdateLogger(Action<IConfigurationBuilder> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            // The following pattern fires the reload token only once per settings change           
            var configurationBuilder = new ConfigurationBuilder();
            try
            {
                configure(configurationBuilder);
                IConfiguration configuration = configurationBuilder.Build();

                // Release previous callback - will be released only if this line is reached, allowing for another reload
                _changeCallback?.Dispose();
                _changeCallback = null;
                // .NET will not trigger a reload for invalid config file format, so reaching this line signals Json is OK
                _changeCallback = configuration.GetReloadToken().RegisterChangeCallback(state =>
                {
                    UpdateLogger(configure);
                }, null);

                // Reading configuration will fail for invalid properties, that is why reload registration must happen
                // before this line or subsequent file updates may not be detected
                global::Serilog.ILogger newLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

                Log.Logger = newLogger;

                GetLogger().Here(l => l.LogInformation("Updated logger: {@configuration}", configuration));
            }
            catch (Exception ex)
            {
                GetLogger().Here(l => l.LogError(ex, ex.Message));
            }
        }

        private static global::Microsoft.Extensions.Logging.ILogger GetLogger()
        {
            return CreateLoggerFactory().CreateLogger(typeof(SerilogHelper).FullName);
        }

        public static global::Microsoft.Extensions.Logging.ILoggerFactory CreateLoggerFactory()
        {
            return new SerilogLoggerFactory();
        }

        private static IDisposable _changeCallback;
    }
}
