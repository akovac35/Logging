// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Denis Kavčič

namespace com.github.akovac35.Logging.Correlation
{
    public class CorrelationProviderAccessor
    {
        public ICorrelationProvider? Current
        {
            get
            {
                return CorrelationProvider.CurrentCorrelationProvider;
            }
        }
    }
}
