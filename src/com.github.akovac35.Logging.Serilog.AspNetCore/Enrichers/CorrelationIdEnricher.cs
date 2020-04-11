// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using com.github.akovac35.Logging.AspNetCore;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace com.github.akovac35.Logging.Serilog.AspNetCore.Enrichers
{
    public class CorrelationIdEnricher : ILogEventEnricher
    {
        public CorrelationIdEnricher()
        {
            _contextAccessor = new HttpContextAccessor();
        }

        protected IHttpContextAccessor _contextAccessor;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            string correlationId = _contextAccessor.GetCorrelationId();
            var tmp = new LogEventProperty(Constants.CorrelationId, new ScalarValue(correlationId ?? ""));
            // Property may be already set with BeginScope
            logEvent.AddPropertyIfAbsent(tmp);
        }
    }
}
