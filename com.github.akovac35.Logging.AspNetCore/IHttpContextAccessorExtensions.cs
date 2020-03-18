// Author: Aleksander Kovač

using com.github.akovac35.Logging.Correlation;
using Microsoft.AspNetCore.Http;

namespace com.github.akovac35.Logging.AspNetCore
{
    public static class IHttpContextAccessorExtensions
    {
        public static string GetCorrelationId(this IHttpContextAccessor contextAccessor)
        {
            ICorrelationProvider correlationProvider = contextAccessor.HttpContext?.RequestServices?.GetService(typeof(ICorrelationProvider)) as ICorrelationProvider;
            return correlationProvider?.GetCorrelationId();
        }

        public static ICorrelationProvider GetCorrelationProvider(this IHttpContextAccessor contextAccessor)
        {
            return contextAccessor.HttpContext?.RequestServices?.GetService(typeof(ICorrelationProvider)) as ICorrelationProvider;
        }
    }
}
