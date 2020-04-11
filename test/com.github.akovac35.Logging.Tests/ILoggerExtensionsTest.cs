// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using com.github.akovac35.Logging.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Collections.Generic;
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
            var logger = new TestLogger("", sink, enabled: true);

            logger.Here(l => l.LogInformation(""));
            StackFrame stackFrame = new StackFrame(true);

            var context = sink.Scopes.ToArray()[0].Scope as System.Collections.Generic.KeyValuePair<string, object>[];

            Assert.IsNotNull(context);

            Assert.AreEqual(Constants.CallerMemberName, context[0].Key);
            Assert.AreEqual(MethodInfo.GetCurrentMethod().Name, context[0].Value);

            Assert.AreEqual(Constants.CallerLineNumber, context[2].Key);
            Assert.AreEqual(stackFrame.GetFileLineNumber() - 1, context[2].Value);
        }

        public static IEnumerable<object> EnteringParameterCases()
        {
            yield return new TestCaseData(new { Amount = 108, Message = "Hello" }).Returns("Entering: { Amount = 108, Message = Hello }");
            yield return new TestCaseData(new TestingFake()).Returns("Entering: " + typeof(TestingFake).FullName);
        }

        [Test, TestCaseSource("EnteringParameterCases")]
        public string Entering_ParameterSerialization_IsValid(object parameter)
        {
            var sink = new TestSink();
            var logger = new TestLogger("", sink, enabled: true);

            logger.Here(l => l.Entering(parameter));

            return sink.Writes.ToArray()[0].Message;
        }

        [Test]
        public void Entering_WithoutParameters_IsValid()
        {
            var sink = new TestSink();
            var logger = new TestLogger("", sink, enabled: true);

            logger.Here(l => l.Entering());
            Assert.AreEqual("Entering", sink.Writes.ToArray()[0].Message);
        }

        public static IEnumerable<object> ExitingParameterCases()
        {
            yield return new TestCaseData(new { Amount = 108, Message = "Hello" }).Returns("Exiting: { Amount = 108, Message = Hello }");
            yield return new TestCaseData(new TestingFake()).Returns("Exiting: " + typeof(TestingFake).FullName);
        }

        [Test, TestCaseSource("ExitingParameterCases")]
        public string Exiting_ParameterSerialization_IsValid(object parameter)
        {
            var sink = new TestSink();
            var logger = new TestLogger("", sink, enabled: true);

            logger.Here(l => l.Exiting(parameter));

            return sink.Writes.ToArray()[0].Message;
        }

        [Test]
        public void Exiting_WithoutParameters_IsValid()
        {
            var sink = new TestSink();
            var logger = new TestLogger("", sink, enabled: true);

            logger.Here(l => l.Exiting());
            Assert.AreEqual("Exiting", sink.Writes.ToArray()[0].Message);
        }

        class TestingFake
        {
            public int Amount { get; set; } = 108;

            public string Message { get; set; } = "Hello";
        }
    }
}
