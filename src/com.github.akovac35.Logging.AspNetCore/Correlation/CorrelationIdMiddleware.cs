// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using com.github.akovac35.Logging.Correlation;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace com.github.akovac35.Logging.AspNetCore.Correlation
{
    public class CorrelationIdMiddleware
    {
        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private readonly RequestDelegate _next;

        public string CorrelationIdHeaderName { get; set; } = "x-request-id";

        public async Task InvokeAsync(HttpContext context)
        {
            ICorrelationProvider correlationProvider = context.GetCorrelationProvider();
            Microsoft.Extensions.Primitives.StringValues headerValue;

            string lowerCaseCorrelationIdHeaderName = CorrelationIdHeaderName.ToLower();
            headerValue = context.Request.Headers.FirstOrDefault(x => x.Key.ToLower() == lowerCaseCorrelationIdHeaderName).Value;

            if (headerValue.Count > 0)
            {
                correlationProvider.SetCorrelationId(headerValue.First());
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
