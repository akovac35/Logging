// Author: Aleksander Kovač

using com.github.akovac35.Logging.AspNetCore;
using Microsoft.AspNetCore.Http;
using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using System.Text;

namespace com.github.akovac35.Logging.NLog.AspNetCore
{
    [LayoutRenderer("CorrelationId")]
    [ThreadSafe]
    public class CorrelationIdLayoutRenderer : LayoutRenderer
    {
        public CorrelationIdLayoutRenderer()
        {
            _contextAccessor = new HttpContextAccessor();
        }

        protected IHttpContextAccessor _contextAccessor;

        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            // Try to get the value from MDLC
            object correlationId = MappedDiagnosticsLogicalContext.GetObject(Constants.CorrelationId);
            if (correlationId == null)
            {
                correlationId = _contextAccessor.GetCorrelationId();
            }
            builder.Append(correlationId ?? "");
        }
    }
}
