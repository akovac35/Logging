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
using System.IO;
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
            yield return new object[] { nameof(EnteringValueTypes), EnteringValueTypes, @"l.Entering(1, 2, 3)", "Entering: [1, 2, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> EnteringValueTypesWithLogLevel = (l) => l.Entering(LogLevel.Information, 1, 2, 3);
            yield return new object[] { nameof(EnteringValueTypesWithLogLevel), EnteringValueTypesWithLogLevel, @"l.Entering(LogLevel.Information, 1, 2, 3)", "Entering: [1, 2, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> EnteringMixedTypes = (l) => l.Entering(1, typeof(int), 3);
            yield return new object[] { nameof(EnteringMixedTypes), EnteringMixedTypes, @"l.Entering(1, typeof(int), 3)", "Entering: [1, System.Int32, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> EnteringMixedTypesWithLogLevel = (l) => l.Entering(LogLevel.Information, 1, typeof(int), 3);
            yield return new object[] { nameof(EnteringMixedTypesWithLogLevel), EnteringMixedTypesWithLogLevel, @"l.Entering(LogLevel.Information, 1, typeof(int), 3)", "Entering: [1, System.Int32, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> Entering = (l) => l.Entering();
            yield return new object[] { nameof(Entering), Entering, @"l.Entering()", "Entering" };

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatValueTypes = (l) => l.EnteringSimpleFormat(1, 2, 3);
            yield return new object[] { nameof(EnteringSimpleFormatValueTypes), EnteringSimpleFormatValueTypes, @"l.EnteringSimpleFormat(1, 2, 3)", "Entering: [1, 2, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatValueTypesWithLogLevel = (l) => l.EnteringSimpleFormat(LogLevel.Information, 1, 2, 3);
            yield return new object[] { nameof(EnteringSimpleFormatValueTypesWithLogLevel), EnteringSimpleFormatValueTypesWithLogLevel, @"l.EnteringSimpleFormat(LogLevel.Information, 1, 2, 3)", "Entering: [1, 2, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatMixedTypes = (l) => l.EnteringSimpleFormat(1, typeof(int), 3);
            yield return new object[] { nameof(EnteringSimpleFormatMixedTypes), EnteringSimpleFormatMixedTypes, @"l.EnteringSimpleFormat(1, typeof(int), 3)", "Entering: [1, \"System.Int32\", 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> EnteringSimpleFormatMixedTypesWithLogLevel = (l) => l.EnteringSimpleFormat(LogLevel.Information, 1, typeof(int), 3);
            yield return new object[] { nameof(EnteringSimpleFormatMixedTypesWithLogLevel), EnteringSimpleFormatMixedTypesWithLogLevel, @"l.EnteringSimpleFormat(LogLevel.Information, 1, typeof(int), 3)", "Entering: [1, \"System.Int32\", 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> ExitingValueTypes = (l) => l.Exiting(1, 2, 3);
            yield return new object[] { nameof(ExitingValueTypes), ExitingValueTypes, @"l.Exiting(1, 2, 3)", "Exiting: [1, 2, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> ExitingValueTypesWithLogLevel = (l) => l.Exiting(LogLevel.Information, 1, 2, 3);
            yield return new object[] { nameof(ExitingValueTypesWithLogLevel), ExitingValueTypesWithLogLevel, @"l.Exiting(LogLevel.Information, 1, 2, 3)", "Exiting: [1, 2, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> ExitingMixedTypes = (l) => l.Exiting(1, typeof(int), 3);
            yield return new object[] { nameof(ExitingMixedTypes), ExitingMixedTypes, @"l.Exiting(1, typeof(int), 3)", "Exiting: [1, System.Int32, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> ExitingMixedTypesWithLogLevel = (l) => l.Exiting(LogLevel.Information, 1, typeof(int), 3);
            yield return new object[] { nameof(ExitingMixedTypesWithLogLevel), ExitingMixedTypesWithLogLevel, @"l.Exiting(LogLevel.Information, 1, typeof(int), 3)", "Exiting: [1, System.Int32, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> Exiting = (l) => l.Exiting();
            yield return new object[] { nameof(Exiting), Exiting, @"l.Exiting()", "Exiting" };

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatValueTypes = (l) => l.ExitingSimpleFormat(1, 2, 3);
            yield return new object[] { nameof(ExitingSimpleFormatValueTypes), ExitingSimpleFormatValueTypes, @"l.ExitingSimpleFormat(1, 2, 3)", "Exiting: [1, 2, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatValueTypesWithLogLevel = (l) => l.ExitingSimpleFormat(LogLevel.Information, 1, 2, 3);
            yield return new object[] { nameof(ExitingSimpleFormatValueTypesWithLogLevel), ExitingSimpleFormatValueTypesWithLogLevel, @"l.ExitingSimpleFormat(LogLevel.Information, 1, 2, 3)", "Exiting: [1, 2, 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatMixedTypes = (l) => l.ExitingSimpleFormat(1, typeof(int), 3);
            yield return new object[] { nameof(ExitingSimpleFormatMixedTypes), ExitingSimpleFormatMixedTypes, @"l.ExitingSimpleFormat(1, typeof(int), 3)", "Exiting: [1, \"System.Int32\", 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> ExitingSimpleFormatMixedTypesWithLogLevel = (l) => l.ExitingSimpleFormat(LogLevel.Information, 1, typeof(int), 3);
            yield return new object[] { nameof(ExitingSimpleFormatMixedTypesWithLogLevel), ExitingSimpleFormatMixedTypesWithLogLevel, @"l.ExitingSimpleFormat(LogLevel.Information, 1, typeof(int), 3)", "Exiting: [1, \"System.Int32\", 3]" };

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationWithType = (l) => l.LogInformation("Testing: {@0}, {@1}", new TestType(), "next arg");
            yield return new object[] { nameof(LogInformationWithType), LogInformationWithType, @"l.LogInformation(""Testing: {@0}, {@1}"", new TestType(), ""next arg"")", "Testing: TestType { TestString: \"xyz\", TestNumber: \"123\" }, \"next arg\"" };

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationWithTypeSimpleFormat = (l) => l.LogInformation("Testing: {0}, {1}", new TestType(), "next arg");
            yield return new object[] { nameof(LogInformationWithTypeSimpleFormat), LogInformationWithTypeSimpleFormat, @"l.LogInformation(""Testing: {0}, {1}"", new TestType(), ""next arg"")", "Testing: \"com.github.akovac35.Logging.Serilog.Tests.TestType\", \"next arg\"" };

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationWithAnonType = (l) => l.LogInformation("Testing: {@0}", new { Amount = 108, Message = "Hello" });
            yield return new object[] { nameof(LogInformationWithAnonType), LogInformationWithAnonType, @"l.LogInformation(""Testing: {@0}"", new { Amount = 108, Message = ""Hello"" })", "Testing: { Amount: 108, Message: \"Hello\" }" };

            Action<Microsoft.Extensions.Logging.ILogger> SimpleLogInformationWithAnonType = (l) => l.LogInformation("Testing: {0}", new { Amount = 108, Message = "Hello" });
            yield return new object[] { nameof(SimpleLogInformationWithAnonType), SimpleLogInformationWithAnonType, @"l.LogInformation(""Testing: {0}"", new { Amount = 108, Message = ""Hello"" })", "Testing: \"{ Amount = 108, Message = Hello }\"" };

            Action<Microsoft.Extensions.Logging.ILogger> LogInformationArray = (l) => l.LogInformation("Testing: {@0}", new object[] { new TestType[] { new TestType(), new TestType() } });
            yield return new object[] { nameof(LogInformationArray), LogInformationArray, @"l.LogInformation(""Testing: {@0}"", new object[] { new TestType[] { new TestType(), new TestType() } })", "Testing: [TestType { TestString: \"xyz\", TestNumber: \"123\" }, TestType { TestString: \"xyz\", TestNumber: \"123\" }]" };

            Action<Microsoft.Extensions.Logging.ILogger> SimpleLogInformationArray = (l) => l.LogInformation("Testing: {0}", new object[] { new TestType[] { new TestType(), new TestType() } });
            yield return new object[] { nameof(SimpleLogInformationArray), SimpleLogInformationArray, @"l.LogInformation(""Testing: {0}"", new object[] { new TestType[] { new TestType(), new TestType() } })", "Testing: [\"com.github.akovac35.Logging.Serilog.Tests.TestType\", \"com.github.akovac35.Logging.Serilog.Tests.TestType\"]" };

            // https://docs.microsoft.com/en-us/dotnet/api/system.string.format?view=netcore-3.1#System_String_Format_System_String_System_Object___
            // Will log only the first array item
            Action<Microsoft.Extensions.Logging.ILogger> LogInformationArrayIncorrect = (l) => l.LogInformation("Testing: {@0}", new TestType[] { new TestType(), new TestType() });
            yield return new object[] { nameof(LogInformationArrayIncorrect), LogInformationArrayIncorrect, @"l.LogInformation(""Testing: {@0}"", new TestType[] { new TestType(), new TestType() })", "Testing: TestType { TestString: \"xyz\", TestNumber: \"123\" }" };

            // https://docs.microsoft.com/en-us/dotnet/api/system.string.format?view=netcore-3.1#System_String_Format_System_String_System_Object___
            // Will log only the first array item
            Action<Microsoft.Extensions.Logging.ILogger> SimpleLogInformationArrayIncorrect = (l) => l.LogInformation("Testing: {0}", new TestType[] { new TestType(), new TestType() });
            yield return new object[] { nameof(SimpleLogInformationArrayIncorrect), SimpleLogInformationArrayIncorrect, @"l.LogInformation(""Testing: {0}"", new TestType[] { new TestType(), new TestType() })", "Testing: \"com.github.akovac35.Logging.Serilog.Tests.TestType\"" };
        }

        [Test, TestCaseSource("LoggerMessage_IsCorrect_Cases")]
        public void LoggerMessage_IsCorrect(string name, Action<Microsoft.Extensions.Logging.ILogger> logAction, string method, string expectedResult)
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

            TestContext.WriteLine($"Method: {method} Template: {InMemorySink.Instance.LogEvents.First().MessageTemplate} Render: {InMemorySink.Instance.LogEvents.First().RenderMessage()}");

            var templateFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "templates.txt");
            File.AppendAllLines(templateFile, new string[]
            {
                $"<tr><td>{method}</td><td>{InMemorySink.Instance.LogEvents.First().RenderMessage()}</td></tr>"
            });

            newLogger = null;
            Log.CloseAndFlush();
            InMemorySink.Instance.Dispose();

            Assert.AreEqual(expectedResult, result);
        }
    }
}
