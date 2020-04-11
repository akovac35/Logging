// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using System.Text;

namespace com.github.akovac35.Logging.NLog.LayoutRenderers
{
    [LayoutRenderer("CorrelationId")]
    [ThreadSafe]
    public class CorrelationIdLayoutRenderer : LayoutRenderer
    {
        public CorrelationIdLayoutRenderer()
        {
        }

        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            // Try to get the value from MDLC
            object correlationId = MappedDiagnosticsLogicalContext.GetObject(Constants.CorrelationId);
            builder.Append(correlationId ?? "");
        }
    }
}
