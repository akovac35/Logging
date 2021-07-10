// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using com.github.akovac35.Logging.Correlation;

namespace com.github.akovac35.Logging.Testing
{
    public class TestCorrelationProviderAccessor : CorrelationProviderAccessor
    {
        public override ICorrelationProvider Current => _correlationProviderInstance;

        private CorrelationProvider _correlationProviderInstance = new CorrelationProvider();

    }
}
