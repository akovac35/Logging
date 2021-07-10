// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using com.github.akovac35.Logging.Correlation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace com.github.akovac35.Logging.Testing
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Remember to clear the TestSink regularly to avoid out-of-memory errors, or simply have a per-test level IServiceProvider.
        /// </summary>
        public static IServiceCollection AddTestLogger(this IServiceCollection services, Action<WriteContext> onWrite = null, Action<ScopeContext> onBeginScope = null, Func<WriteContext, bool> writeEnabled = null, Func<ScopeContext, bool> beginEnabled = null)
        {
            services.TryAddSingleton<ITestSink>(fact =>
            {
                var sink = new TestSink(writeEnabled, beginEnabled);
                sink.MessageLogged += onWrite;
                sink.ScopeStarted += onBeginScope;

                return sink;
            });
            services.TryAddSingleton<TestLoggerFactory>(fact =>
            {
                var sink = fact.GetRequiredService<ITestSink>();
                return new TestLoggerFactory(sink);
            });
            services.TryAddSingleton<ILoggerFactory>(fact =>
            {
                return fact.GetRequiredService<TestLoggerFactory>();
            });
            services.TryAddSingleton(typeof(ILogger<>), typeof(TestLogger<>));
            services.TryAddScoped<CorrelationProviderAccessor, TestCorrelationProviderAccessor>();

            return services;
        }
    }
}
