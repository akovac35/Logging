// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using com.github.akovac35.Logging.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Diagnostics;
using System.Reflection;

namespace com.github.akovac35.Logging.Tests
{
    [TestFixture]
    public class ILoggerExtensionsTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Here_LoggerScope_Exists()
        {
            var sink = new TestSink();
            var logger = new TestLogger("", sink);

            logger.Here(l => l.LogInformation(""));
            StackFrame stackFrame = new StackFrame(true);

            var context = sink.Scopes.ToArray()[0].State as System.Collections.Generic.KeyValuePair<string, object>[];

            Assert.IsNotNull(context);

            Assert.AreEqual(Constants.CallerMemberName, context[0].Key);
            Assert.AreEqual(MethodInfo.GetCurrentMethod().Name, context[0].Value);

            Assert.AreEqual(Constants.CallerLineNumber, context[2].Key);
            Assert.AreEqual(stackFrame.GetFileLineNumber() - 1, context[2].Value);
        }
    }
}
