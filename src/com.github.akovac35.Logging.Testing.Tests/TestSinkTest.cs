// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace com.github.akovac35.Logging.Testing.Tests
{
    [TestFixture]
    public class TestSinkTest
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
        public void TestClear()
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var testTypeInstance = serviceProvider.GetRequiredService<TestType>();
            testTypeInstance.TestFunctionReturningBool(1, "1");
            testTypeInstance.TestFunctionPassingArgumentsToLoggerScopes(2, "2");

            var testSink = serviceProvider.GetRequiredService<ITestSink>();
            testSink.Clear();

            Assert.IsTrue(testSink.Writes.Count == 0);
            Assert.IsTrue(testSink.Scopes.Count == 0);
            Assert.IsTrue(testSink.CurrentScope == null);
        }
    }
}
