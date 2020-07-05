// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace com.github.akovac35.Logging.Testing.Tests
{
    public class TestType
    {
        public TestType(ILogger<TestType> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private ILogger _logger;

        public bool TestFunctionReturningBool(int a, string b)
        {
            _logger.Here(l => l.Entering(a, b));

            var result = true;

            _logger.Here(l => l.Exiting(result));
            return result;
        }

        public void TestFunctionPassingArgumentsToLoggerScopes(int a, string b)
        {
            _logger.Here(l => l.Entering(a, b));

            using (_logger.BeginScope(new[] { new KeyValuePair<string, object>("a", a) }))
            {
                _logger.Here(l => l.LogInformation("Message inside first explicit logger scope."));

                using (_logger.BeginScope(new[] { new KeyValuePair<string, object>("b", b) }))
                {
                    _logger.Here(l => l.LogInformation("Message inside second explicit logger scope."));
                }
            }

            _logger.Here(l => l.Exiting());
        }
    }
}
