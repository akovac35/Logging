using com.github.akovac35.Logging.Tests.Shared;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Reflection;

namespace com.github.akovac35.Logging.Tests
{
    [TestFixture]
    public class LoggerHelperTests
    {
        public static string TypeFullName { get; } = typeof(LoggerHelperTests).FullName;

        [OneTimeSetUp]
        public void Setup()
        {
        }

        [Test]
        public void AssertHereContextIsSet()
        {
            var sink = new TestSink();
            var logger = new TestLogger(TypeFullName, sink, enabled: true);

            LoggerHelper<LoggerHelperTests>.Logger = logger;
            LoggerHelper<LoggerHelperTests>.Here(l => l.LogInformation(""));

            var context = sink.Scopes.ToArray()[0].Scope as System.Collections.Generic.KeyValuePair<string, object>[];

            Assert.IsNotNull(context);
            
            Assert.AreEqual(Constants.CallerMemberName, context[0].Key);
            Assert.AreEqual(MethodInfo.GetCurrentMethod().Name, context[0].Value);

            Assert.AreEqual(Constants.CallerLineNumber, context[2].Key);
            Assert.AreNotEqual(-1, context[2].Value);
        }
    }
}