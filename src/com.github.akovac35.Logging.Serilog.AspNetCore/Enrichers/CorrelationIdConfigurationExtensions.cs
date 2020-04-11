// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using Serilog;
using Serilog.Configuration;
using System;

namespace com.github.akovac35.Logging.Serilog.AspNetCore.Enrichers
{
    public static class CorrelationIdConfigurationExtensions
    {
        public static LoggerConfiguration WithCorrelationId(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<CorrelationIdEnricher>();
        }
    }
}
