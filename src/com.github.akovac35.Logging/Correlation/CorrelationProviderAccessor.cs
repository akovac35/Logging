// License:
// Apache License Version 2.0, January 2004

// Authors:
//  Denis Kavčič
//  Aleksander Kovač

namespace com.github.akovac35.Logging.Correlation
{
    public class CorrelationProviderAccessor
    {
        public virtual ICorrelationProvider? Current
        {
            get
            {
                return CorrelationProvider.CurrentCorrelationProvider;
            }
        }
    }
}
