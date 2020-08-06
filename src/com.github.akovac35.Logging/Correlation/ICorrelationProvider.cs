// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač
//   Denis Kavčič

namespace com.github.akovac35.Logging.Correlation
{
    public interface ICorrelationProvider
    {
        public Correlation Value { get; }
        string? HeaderName { get; }

        public string GetCorrelationId();

        public void SetCorrelationId(string id);
    }
}
