// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač
//   Denis Kavčič

using System;
using System.Net.Http.Headers;
using System.Threading;

namespace com.github.akovac35.Logging.Correlation
{
    public class CorrelationProvider : ICorrelationProvider
    {
        private static AsyncLocal<ICorrelationProvider?> _currentCorrelationProvider = new AsyncLocal<ICorrelationProvider?>();

        /// <summary>
        /// Accesses the ambient (AsyncLocal) ICorrelationProvider.
        /// </summary>
        public static ICorrelationProvider? CurrentCorrelationProvider
        {
            get
            {
                return _currentCorrelationProvider.Value;
            }
            set
            {
                _currentCorrelationProvider.Value = value;
            }
        }

        public CorrelationProvider()
        {
            Value = new Correlation();
        }

        public CorrelationProvider(Correlation correlation)
        {
            Value = correlation ?? throw new ArgumentNullException(nameof(correlation));
        }

        public CorrelationProvider(string headerName)
        {
            HeaderName = headerName ?? throw new ArgumentNullException(nameof(headerName));
            Value = new Correlation();
        }

        public CorrelationProvider(Correlation correlation, string headerName)
        {
            HeaderName = headerName ?? throw new ArgumentNullException(nameof(headerName));
            Value = correlation ?? throw new ArgumentNullException(nameof(correlation));
        }

        public Correlation Value { get; protected set; }

        public string HeaderName { get; protected set; } = "x-request-id";

        public string GetCorrelationId()
        {
            return Value.Id;
        }

        public void SetCorrelationId(string id)
        {
            Value.Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}
