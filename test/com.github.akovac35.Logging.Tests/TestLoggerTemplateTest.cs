// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using com.github.akovac35.Logging.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace com.github.akovac35.Logging.Tests
{
    [TestFixture]
    public class TestLoggerTemplateTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [SetUp]
        public void SetUp()
        {
        }

        public static IEnumerable<object> LoggerMessage_IsCorrect_Cases()
        {
            Action<Microsoft.Extensions.Logging.ILogger> EnteringValueTypes = (l) => l.Entering(1, 2, 3);
            yield return new TestCaseData(nameof(EnteringValueTypes), EnteringValueTypes).Returns("Entering: 1, 2, 3");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringValueTypesWithLogLevel = (l) => l.Entering(Microsoft.Extensions.Logging.LogLevel.Information, 1, 2, 3);
            yield return new TestCaseData(nameof(EnteringValueTypesWithLogLevel), EnteringValueTypesWithLogLevel).Returns("Entering: 1, 2, 3");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringMixedTypes = (l) => l.Entering(1, typeof(int), 3);
            yield return new TestCaseData(nameof(EnteringMixedTypes), EnteringMixedTypes).Returns("Entering: 1, System.Int32, 3");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringMixedTypesWithLogLevel = (l) => l.Entering(Microsoft.Extensions.Logging.LogLevel.Information, 1, typeof(int), 3);
            yield return new TestCaseData(nameof(EnteringMixedTypesWithLogLevel), EnteringMixedTypesWithLogLevel).Returns("Entering: 1, System.Int32, 3");

            Action<Microsoft.Extensions.Logging.ILogger> Entering = (l) => l.Entering();
            yield return new TestCaseData(nameof(Entering), Entering).Returns("Entering");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatValueTypes = (l) => l.EnteringSimpleFormat(1, 2, 3);
            yield return new TestCaseData(nameof(EnteringSimpleFormatValueTypes), EnteringSimpleFormatValueTypes).Returns("Entering: 1, 2, 3");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatValueTypesWithLogLevel = (l) => l.EnteringSimpleFormat(Microsoft.Extensions.Logging.LogLevel.Information, 1, 2, 3);
            yield return new TestCaseData(nameof(EnteringSimpleFormatValueTypesWithLogLevel), EnteringSimpleFormatValueTypesWithLogLevel).Returns("Entering: 1, 2, 3");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatMixedTypes = (l) => l.EnteringSimpleFormat(1, typeof(int), 3);
            yield return new TestCaseData(nameof(EnteringSimpleFormatMixedTypes), EnteringSimpleFormatMixedTypes).Returns("Entering: 1, System.Int32, 3");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatMixedTypesWithLogLevel = (l) => l.EnteringSimpleFormat(Microsoft.Extensions.Logging.LogLevel.Information, 1, typeof(int), 3);
            yield return new TestCaseData(nameof(EnteringSimpleFormatMixedTypesWithLogLevel), EnteringSimpleFormatMixedTypesWithLogLevel).Returns("Entering: 1, System.Int32, 3");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingValueTypes = (l) => l.Exiting(1, 2, 3);
            yield return new TestCaseData(nameof(ExitingValueTypes), ExitingValueTypes).Returns("Exiting: 1, 2, 3");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingValueTypesWithLogLevel = (l) => l.Exiting(Microsoft.Extensions.Logging.LogLevel.Information, 1, 2, 3);
            yield return new TestCaseData(nameof(ExitingValueTypesWithLogLevel), ExitingValueTypesWithLogLevel).Returns("Exiting: 1, 2, 3");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingMixedTypes = (l) => l.Exiting(1, typeof(int), 3);
            yield return new TestCaseData(nameof(ExitingMixedTypes), ExitingMixedTypes).Returns("Exiting: 1, System.Int32, 3");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingMixedTypesWithLogLevel = (l) => l.Exiting(Microsoft.Extensions.Logging.LogLevel.Information, 1, typeof(int), 3);
            yield return new TestCaseData(nameof(ExitingMixedTypesWithLogLevel), ExitingMixedTypesWithLogLevel).Returns("Exiting: 1, System.Int32, 3");

            Action<Microsoft.Extensions.Logging.ILogger> Exiting = (l) => l.Exiting();
            yield return new TestCaseData(nameof(Exiting), Exiting).Returns("Exiting");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatValueTypes = (l) => l.ExitingSimpleFormat(1, 2, 3);
            yield return new TestCaseData(nameof(ExitingSimpleFormatValueTypes), ExitingSimpleFormatValueTypes).Returns("Exiting: 1, 2, 3");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatValueTypesWithLogLevel = (l) => l.ExitingSimpleFormat(Microsoft.Extensions.Logging.LogLevel.Information, 1, 2, 3);
            yield return new TestCaseData(nameof(ExitingSimpleFormatValueTypesWithLogLevel), ExitingSimpleFormatValueTypesWithLogLevel).Returns("Exiting: 1, 2, 3");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatMixedTypes = (l) => l.ExitingSimpleFormat(1, typeof(int), 3);
            yield return new TestCaseData(nameof(ExitingSimpleFormatMixedTypes), ExitingSimpleFormatMixedTypes).Returns("Exiting: 1, System.Int32, 3");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatMixedTypesWithLogLevel = (l) => l.ExitingSimpleFormat(Microsoft.Extensions.Logging.LogLevel.Information, 1, typeof(int), 3);
            yield return new TestCaseData(nameof(ExitingSimpleFormatMixedTypesWithLogLevel), ExitingSimpleFormatMixedTypesWithLogLevel).Returns("Exiting: 1, System.Int32, 3");

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationWithType = (l) => l.LogInformation("Testing: {@0}, {@1}", new TestType(), "next arg");
            yield return new TestCaseData(nameof(LogInformationWithType), LogInformationWithType).Returns("Testing: com.github.akovac35.Logging.Tests.TestType, next arg");

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationWithTypeSimpleFormat = (l) => l.LogInformation("Testing: {0}, {1}", new TestType(), "next arg");
            yield return new TestCaseData(nameof(LogInformationWithTypeSimpleFormat), LogInformationWithTypeSimpleFormat).Returns("Testing: com.github.akovac35.Logging.Tests.TestType, next arg");

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationWithAnonType = (l) => l.LogInformation("Testing: {@0}", new { Amount = 108, Message = "Hello" });
            yield return new TestCaseData(nameof(LogInformationWithAnonType), LogInformationWithAnonType).Returns("Testing: { Amount = 108, Message = Hello }");

            Action<Microsoft.Extensions.Logging.ILogger> SimpleLogInformationWithAnonType = (l) => l.LogInformation("Testing: {0}", new { Amount = 108, Message = "Hello" });
            yield return new TestCaseData(nameof(SimpleLogInformationWithAnonType), SimpleLogInformationWithAnonType).Returns("Testing: { Amount = 108, Message = Hello }");

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationArray = (l) => l.LogInformation("Testing: {@0}", new object[] { new TestType[] { new TestType(), new TestType() } });
            yield return new TestCaseData(nameof(LogInformationArray), LogInformationArray).Returns("Testing: com.github.akovac35.Logging.Tests.TestType, com.github.akovac35.Logging.Tests.TestType");

            Action<Microsoft.Extensions.Logging.ILogger> SimpleLogInformationArray = (l) => l.LogInformation("Testing: {0}", new object[] { new TestType[] { new TestType(), new TestType() } });
            yield return new TestCaseData(nameof(SimpleLogInformationArray), SimpleLogInformationArray).Returns("Testing: com.github.akovac35.Logging.Tests.TestType, com.github.akovac35.Logging.Tests.TestType");

            // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
            // Will log only the first array item
            Action<Microsoft.Extensions.Logging.ILogger> LogInformationArrayIncorrect = (l) => l.LogInformation("Testing: {@0}", new TestType[] { new TestType(), new TestType() });
            yield return new TestCaseData(nameof(LogInformationArrayIncorrect), LogInformationArrayIncorrect).Returns("Testing: com.github.akovac35.Logging.Tests.TestType");

            // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
            // Will log only the first array item
            Action<Microsoft.Extensions.Logging.ILogger> SimpleLogInformationArrayIncorrect = (l) => l.LogInformation("Testing: {0}", new TestType[] { new TestType(), new TestType() });
            yield return new TestCaseData(nameof(SimpleLogInformationArrayIncorrect), SimpleLogInformationArrayIncorrect).Returns("Testing: com.github.akovac35.Logging.Tests.TestType");
        }

        [Test, TestCaseSource("LoggerMessage_IsCorrect_Cases")]
        public string LoggerMessage_IsCorrect(string name, Action<Microsoft.Extensions.Logging.ILogger> logAction)
        {
            var sink = new TestSink();
            var logger = new TestLogger("", sink, enabled: true);

            logger.Here(logAction);

            return sink.Writes.ToArray()[0].Message;
        }
    }
}
