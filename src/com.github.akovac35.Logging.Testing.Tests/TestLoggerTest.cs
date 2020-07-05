// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;

namespace com.github.akovac35.Logging.Testing.Tests
{
    [TestFixture]
    public class TestLoggerTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            customOnWrite = writeContext => TestContext.WriteLine(writeContext);

            serviceCollection = new ServiceCollection();
            serviceCollection.AddTestLogger(onWrite: customOnWrite);
            serviceCollection.AddTransient<TestType>();
        }

        [SetUp]
        public void SetUp()
        {
        }

        private IServiceCollection serviceCollection;

        private Action<WriteContext> customOnWrite;

        [Test]
        public void TestMessages()
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var testTypeInstance = serviceProvider.GetRequiredService<TestType>();
            testTypeInstance.TestFunctionReturningBool(1, "1");

            var testSink = serviceProvider.GetRequiredService<ITestSink>();
            // Entry, Exit
            Assert.IsTrue(testSink.Writes.Count == 2);
            Assert.AreEqual("TestFunctionReturningBool", testSink.Writes.First().Scope.KvState?.First().Value);
        }

        [Test]
        public void TestLoggerScopes()
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var testTypeInstance = serviceProvider.GetRequiredService<TestType>();
            testTypeInstance.TestFunctionPassingArgumentsToLoggerScopes(1, "1");

            var testSink = serviceProvider.GetRequiredService<ITestSink>();
            // 4 scopes of Here and 2 explicit scopes
            Assert.IsTrue(testSink.Scopes.Count == 6);
        }
    }
}
