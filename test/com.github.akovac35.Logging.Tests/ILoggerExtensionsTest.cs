using com.github.akovac35.Logging.Tests.Shared;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Reflection;

namespace com.github.akovac35.Logging.Tests
{
    [TestFixture]
    public class ILoggerExtensionsTest
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

            logger.Here(l => l.LogInformation(""));

            var context = sink.Scopes.ToArray()[0].Scope as System.Collections.Generic.KeyValuePair<string, object>[];

            Assert.IsNotNull(context);

            Assert.AreEqual(Constants.CallerMemberName, context[0].Key);
            Assert.AreEqual(MethodInfo.GetCurrentMethod().Name, context[0].Value);

            Assert.AreEqual(Constants.CallerLineNumber, context[2].Key);
            Assert.AreNotEqual(-1, context[2].Value);
        }

        [Test]
        public void EnteringTests()
        {
            var sink = new TestSink();
            var logger = new TestLogger(TypeFullName, sink, enabled: true);

            logger.Here(l => l.Entering());
            Assert.AreEqual("Entering", sink.Writes.ToArray()[0].Message);

            var x = new  { Amount = 108, Message = "Hello" };

            logger.Here(l => l.Entering(new { Amount = 108, Message = "Hello" }));
            Assert.AreEqual("Entering: { Amount = 108, Message = Hello }", sink.Writes.ToArray()[1].Message);

            logger.Here(l => l.EnteringSimpleFormat(new Testing()));
            Assert.AreEqual("Entering: " + typeof(Testing).FullName, sink.Writes.ToArray()[2].Message);
        }

        [Test]
        public void ExitingTests()
        {
            var sink = new TestSink();
            var logger = new TestLogger(TypeFullName, sink, enabled: true);

            logger.Here(l => l.Exiting());
            Assert.AreEqual("Exiting", sink.Writes.ToArray()[0].Message);

            var x = new { Amount = 108, Message = "Hello" };

            logger.Here(l => l.Exiting(new { Amount = 108, Message = "Hello" }));
            Assert.AreEqual("Exiting: { Amount = 108, Message = Hello }", sink.Writes.ToArray()[1].Message);

            logger.Here(l => l.ExitingSimpleFormat(new Testing()));
            Assert.AreEqual("Exiting: " + typeof(Testing).FullName, sink.Writes.ToArray()[2].Message);
        }

        class Testing
        {
            public int Amount { get; set; }
            public string Message { get; set; }
        }
    }
}
