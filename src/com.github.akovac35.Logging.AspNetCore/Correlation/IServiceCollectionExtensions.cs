// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač
//   Denis Kavčič

using com.github.akovac35.Logging.Correlation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace com.github.akovac35.Logging.AspNetCore.Correlation
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddLoggingCorrelation(this IServiceCollection services, string correlationIdHeaderName = "x-request-id", bool obtainCorrelationIdFromRequestHeaders = false)
        {
            services.TryAddScoped<ICorrelationProvider>(ILoggerFactory => {
                var provider = new CorrelationProvider(correlationIdHeaderName);
                return provider;
            });
            services.TryAddScoped<CorrelationProviderAccessor>();
            services.TryAddScoped<LoggingCorrelationMiddleware>(fact => {
                return new LoggingCorrelationMiddleware(fact.GetRequiredService<ICorrelationProvider>(), fact.GetRequiredService<ILogger<LoggingCorrelationMiddleware>>(), correlationIdHeaderName, obtainCorrelationIdFromRequestHeaders);
            });

            return services;
        }
    }
}
