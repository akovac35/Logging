// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using com.github.akovac35.Logging.Correlation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.github.akovac35.Logging.AspNetCore.Correlation
{
    public class LoggingCorrelationMiddleware: IMiddleware
    {
        public LoggingCorrelationMiddleware(ICorrelationProvider correlationProvider, ILogger<LoggingCorrelationMiddleware> logger, string correlationIdHeaderName = "x-request-id", bool obtainCorrelationIdFromRequestHeaders = false)
        {
            _correlationProvider = correlationProvider ?? throw new ArgumentNullException(nameof(correlationProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            CorrelationIdHeaderName = correlationIdHeaderName ?? throw new ArgumentNullException(nameof(correlationIdHeaderName));
            _obtainCorrelationIdFromRequestHeaders = obtainCorrelationIdFromRequestHeaders;
        }

        private ILogger _logger;
        protected ICorrelationProvider _correlationProvider;
        public string CorrelationIdHeaderName { get; }
        protected bool _obtainCorrelationIdFromRequestHeaders;

        public virtual async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.Here(l => l.Entering(_correlationProvider, CorrelationIdHeaderName, _obtainCorrelationIdFromRequestHeaders));

            if (_obtainCorrelationIdFromRequestHeaders)
            {
                string lowerCaseCorrelationIdHeaderName = CorrelationIdHeaderName.ToLower();
                Microsoft.Extensions.Primitives.StringValues headerValue = context.Request.Headers.FirstOrDefault(x => x.Key.ToLower() == lowerCaseCorrelationIdHeaderName).Value;

                if (headerValue.Count > 0)
                {
                    var correlationValue = headerValue.First();
                    _logger.Here(l => l.LogTrace("Using {@0} header value {@1} as correlation id.", CorrelationIdHeaderName, correlationValue));
                    _correlationProvider.SetCorrelationId(correlationValue);
                } 
            }

            CorrelationProvider.CurrentCorrelationProvider = _correlationProvider;

            using (_logger.BeginScope(new[] { new KeyValuePair<string, object>(Constants.CorrelationId, _correlationProvider.GetCorrelationId()) }))
            {
                await next(context);
            }

            _logger.Here(l => l.Exiting());
        }
    }
}
