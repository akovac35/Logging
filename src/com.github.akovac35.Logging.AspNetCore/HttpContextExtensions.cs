// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using com.github.akovac35.Logging.Correlation;
using Microsoft.AspNetCore.Http;

namespace com.github.akovac35.Logging.AspNetCore
{
    public static class HttpContextExtensions
    {
        public static string GetCorrelationId(this HttpContext context)
        {
            ICorrelationProvider correlationProvider = context.RequestServices?.GetService(typeof(ICorrelationProvider)) as ICorrelationProvider;
            return correlationProvider?.GetCorrelationId();
        }

        public static ICorrelationProvider GetCorrelationProvider(this HttpContext context)
        {
            return context.RequestServices?.GetService(typeof(ICorrelationProvider)) as ICorrelationProvider;
        }
    }
}
