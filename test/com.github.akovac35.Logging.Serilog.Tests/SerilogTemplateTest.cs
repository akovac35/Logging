// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.github.akovac35.Logging.Serilog.Tests
{
    [TestFixture]
    public class SerilogTemplateTest
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
            yield return new TestCaseData(nameof(EnteringValueTypes), EnteringValueTypes).Returns("Entering: [1, 2, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringValueTypesWithLogLevel = (l) => l.Entering(LogLevel.Information, 1, 2, 3);
            yield return new TestCaseData(nameof(EnteringValueTypesWithLogLevel), EnteringValueTypesWithLogLevel).Returns("Entering: [1, 2, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringMixedTypes = (l) => l.Entering(1, typeof(int), 3);
            yield return new TestCaseData(nameof(EnteringMixedTypes), EnteringMixedTypes).Returns("Entering: [1, System.Int32, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringMixedTypesWithLogLevel = (l) => l.Entering(LogLevel.Information, 1, typeof(int), 3);
            yield return new TestCaseData(nameof(EnteringMixedTypesWithLogLevel), EnteringMixedTypesWithLogLevel).Returns("Entering: [1, System.Int32, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> Entering = (l) => l.Entering();
            yield return new TestCaseData(nameof(Entering), Entering).Returns("Entering");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatValueTypes = (l) => l.EnteringSimpleFormat(1, 2, 3);
            yield return new TestCaseData(nameof(EnteringSimpleFormatValueTypes), EnteringSimpleFormatValueTypes).Returns("Entering: [1, 2, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatValueTypesWithLogLevel = (l) => l.EnteringSimpleFormat(LogLevel.Information, 1, 2, 3);
            yield return new TestCaseData(nameof(EnteringSimpleFormatValueTypesWithLogLevel), EnteringSimpleFormatValueTypesWithLogLevel).Returns("Entering: [1, 2, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatMixedTypes = (l) => l.EnteringSimpleFormat(1, typeof(int), 3);
            yield return new TestCaseData(nameof(EnteringSimpleFormatMixedTypes), EnteringSimpleFormatMixedTypes).Returns("Entering: [1, \"System.Int32\", 3]");

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatMixedTypesWithLogLevel = (l) => l.EnteringSimpleFormat(LogLevel.Information, 1, typeof(int), 3);
            yield return new TestCaseData(nameof(EnteringSimpleFormatMixedTypesWithLogLevel), EnteringSimpleFormatMixedTypesWithLogLevel).Returns("Entering: [1, \"System.Int32\", 3]");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingValueTypes = (l) => l.Exiting(1, 2, 3);
            yield return new TestCaseData(nameof(ExitingValueTypes), ExitingValueTypes).Returns("Exiting: [1, 2, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingValueTypesWithLogLevel = (l) => l.Exiting(LogLevel.Information, 1, 2, 3);
            yield return new TestCaseData(nameof(ExitingValueTypesWithLogLevel), ExitingValueTypesWithLogLevel).Returns("Exiting: [1, 2, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingMixedTypes = (l) => l.Exiting(1, typeof(int), 3);
            yield return new TestCaseData(nameof(ExitingMixedTypes), ExitingMixedTypes).Returns("Exiting: [1, System.Int32, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingMixedTypesWithLogLevel = (l) => l.Exiting(LogLevel.Information, 1, typeof(int), 3);
            yield return new TestCaseData(nameof(ExitingMixedTypesWithLogLevel), ExitingMixedTypesWithLogLevel).Returns("Exiting: [1, System.Int32, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> Exiting = (l) => l.Exiting();
            yield return new TestCaseData(nameof(Exiting), Exiting).Returns("Exiting");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatValueTypes = (l) => l.ExitingSimpleFormat(1, 2, 3);
            yield return new TestCaseData(nameof(ExitingSimpleFormatValueTypes), ExitingSimpleFormatValueTypes).Returns("Exiting: [1, 2, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatValueTypesWithLogLevel = (l) => l.ExitingSimpleFormat(LogLevel.Information, 1, 2, 3);
            yield return new TestCaseData(nameof(ExitingSimpleFormatValueTypesWithLogLevel), ExitingSimpleFormatValueTypesWithLogLevel).Returns("Exiting: [1, 2, 3]");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatMixedTypes = (l) => l.ExitingSimpleFormat(1, typeof(int), 3);
            yield return new TestCaseData(nameof(ExitingSimpleFormatMixedTypes), ExitingSimpleFormatMixedTypes).Returns("Exiting: [1, \"System.Int32\", 3]");

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatMixedTypesWithLogLevel = (l) => l.ExitingSimpleFormat(LogLevel.Information, 1, typeof(int), 3);
            yield return new TestCaseData(nameof(ExitingSimpleFormatMixedTypesWithLogLevel), ExitingSimpleFormatMixedTypesWithLogLevel).Returns("Exiting: [1, \"System.Int32\", 3]");

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationWithType = (l) => l.LogInformation("Testing: {@0}, {@1}", new TestType(), "next arg");
            yield return new TestCaseData(nameof(LogInformationWithType), LogInformationWithType).Returns("Testing: TestType { TestString: \"xyz\", TestNumber: \"123\" }, \"next arg\"");

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationWithTypeSimpleFormat = (l) => l.LogInformation("Testing: {0}, {1}", new TestType(), "next arg");
            yield return new TestCaseData(nameof(LogInformationWithTypeSimpleFormat), LogInformationWithTypeSimpleFormat).Returns("Testing: \"com.github.akovac35.Logging.Serilog.Tests.TestType\", \"next arg\"");

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationWithAnonType = (l) => l.LogInformation("Testing: {@0}", new { Amount = 108, Message = "Hello" });
            yield return new TestCaseData(nameof(LogInformationWithAnonType), LogInformationWithAnonType).Returns("Testing: { Amount: 108, Message: \"Hello\" }");

            Action<Microsoft.Extensions.Logging.ILogger> SimpleLogInformationWithAnonType = (l) => l.LogInformation("Testing: {0}", new { Amount = 108, Message = "Hello" });
            yield return new TestCaseData(nameof(SimpleLogInformationWithAnonType), SimpleLogInformationWithAnonType).Returns("Testing: \"{ Amount = 108, Message = Hello }\"");

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationArray = (l) => l.LogInformation("Testing: {@0}", new object[] { new TestType[] { new TestType(), new TestType() } });
            yield return new TestCaseData(nameof(LogInformationArray), LogInformationArray).Returns("Testing: [TestType { TestString: \"xyz\", TestNumber: \"123\" }, TestType { TestString: \"xyz\", TestNumber: \"123\" }]");

            Action<Microsoft.Extensions.Logging.ILogger> SimpleLogInformationArray = (l) => l.LogInformation("Testing: {0}", new object[] { new TestType[] { new TestType(), new TestType() } });
            yield return new TestCaseData(nameof(SimpleLogInformationArray), SimpleLogInformationArray).Returns("Testing: [\"com.github.akovac35.Logging.Serilog.Tests.TestType\", \"com.github.akovac35.Logging.Serilog.Tests.TestType\"]");

            // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
            // Will log only the first array item
            Action<Microsoft.Extensions.Logging.ILogger> LogInformationArrayIncorrect = (l) => l.LogInformation("Testing: {@0}", new TestType[] { new TestType(), new TestType() });
            yield return new TestCaseData(nameof(LogInformationArrayIncorrect), LogInformationArrayIncorrect).Returns("Testing: TestType { TestString: \"xyz\", TestNumber: \"123\" }");

            // https://stackoverflow.com/questions/40885239/using-an-array-as-argument-for-string-format
            // Will log only the first array item
            Action<Microsoft.Extensions.Logging.ILogger> SimpleLogInformationArrayIncorrect = (l) => l.LogInformation("Testing: {0}", new TestType[] { new TestType(), new TestType() });
            yield return new TestCaseData(nameof(SimpleLogInformationArrayIncorrect), SimpleLogInformationArrayIncorrect).Returns("Testing: \"com.github.akovac35.Logging.Serilog.Tests.TestType\"");

            
        }

        [Test, TestCaseSource("LoggerMessage_IsCorrect_Cases")]
        public string LoggerMessage_IsCorrect(string name, Action<Microsoft.Extensions.Logging.ILogger> logAction)
        {
            global::Serilog.ILogger newLogger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.InMemory()
                .CreateLogger();
            Log.Logger = newLogger;

            ILoggerFactory loggerFactory = new SerilogLoggerFactory();
            Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger("test");

            logger.Here(logAction);
            var result =  InMemorySink.Instance.LogEvents.First().RenderMessage();

            newLogger = null;
            Log.CloseAndFlush();
            InMemorySink.Instance.Dispose();
            
            return result;
        }
    }
}
