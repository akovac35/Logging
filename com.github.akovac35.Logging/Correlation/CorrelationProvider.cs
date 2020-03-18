// Author: Aleksander Kovač

using System;

namespace com.github.akovac35.Logging.Correlation
{
    public class CorrelationProvider : ICorrelationProvider
    {
        public CorrelationProvider()
        {
            Value = new Correlation();
        }

        public CorrelationProvider(Correlation correlation)
        {
            if (correlation == null) throw new ArgumentNullException(nameof(correlation));
            Value = correlation;
        }

        public Correlation Value { get; protected set; }

        public string GetCorrelationId()
        {
            return Value.Id;
        }

        public void SetCorrelationId(string id)
        {
            Value.Id = id;
        }
    }
}
