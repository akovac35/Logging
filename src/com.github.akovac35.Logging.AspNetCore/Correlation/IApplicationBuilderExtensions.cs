// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using Microsoft.AspNetCore.Builder;

namespace com.github.akovac35.Logging.AspNetCore.Correlation
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLoggingCorrelation(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggingCorrelationMiddleware>();

            return app;
        }
    }
}
