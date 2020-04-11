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
    public class LoggerHelperTest
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
            LoggerFactoryProvider.LoggerFactory = new TestLoggerFactory(sink, true);
            LoggerHelper<LoggerHelperTest>.Here(l => l.LogInformation(""));
            StackFrame stackFrame = new StackFrame(true);

            var context = sink.Scopes.ToArray()[0].Scope as System.Collections.Generic.KeyValuePair<string, object>[];

            Assert.IsNotNull(context);

            Assert.AreEqual(Constants.CallerMemberName, context[0].Key);
            Assert.AreEqual(MethodInfo.GetCurrentMethod().Name, context[0].Value);

            Assert.AreEqual(Constants.CallerLineNumber, context[2].Key);
            Assert.AreEqual(stackFrame.GetFileLineNumber() - 1, context[2].Value);
        }

        [Test]
        public void Logger_Getter_Works()
        {
            var sink = new TestSink();
            LoggerFactoryProvider.LoggerFactory = new TestLoggerFactory(sink, true);

            // Just confirm that static fields of a generic static class are not shared between constructed types
            Assert.IsInstanceOf<ILogger<A>>(LoggerHelper<A>.Logger);
            Assert.IsInstanceOf<ILogger<B>>(LoggerHelper<B>.Logger);
        }

        class A
        {
        }

        class B
        {
        }
    }
}
