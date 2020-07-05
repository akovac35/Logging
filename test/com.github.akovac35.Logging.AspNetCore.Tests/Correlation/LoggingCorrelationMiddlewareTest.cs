// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using com.github.akovac35.Logging.AspNetCore.Correlation;
using com.github.akovac35.Logging.Correlation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace com.github.akovac35.Logging.AspNetCore.Tests.Correlation
{
    [TestFixture]
    public class LoggingCorrelationMiddlewareTest
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
        public async Task InvokeAsync_FindsCorrelationHeader()
        {
            var correlationId = "1234";
            var context = new DefaultHttpContext();
            var correlationProvider = new CorrelationProvider();
            var correlationMiddleware = new LoggingCorrelationMiddleware(correlationProvider, new NullLogger<LoggingCorrelationMiddleware>(), obtainCorrelationIdFromRequestHeaders: true);

            context.Request.Headers.Add("X-Request-Id", correlationId);
            await correlationMiddleware.InvokeAsync(context, (innerHttpContext) => Task.FromResult(0));

            Assert.AreEqual(correlationId, correlationProvider.GetCorrelationId());
        }

        [Test]
        public async Task InvokeAsync_HeaderDoesNotContainCorrelation()
        {
            var correlationId = "1234";
            var context = new DefaultHttpContext();
            var correlationProvider = new CorrelationProvider();
            correlationProvider.SetCorrelationId(correlationId);
            var correlationMiddleware = new LoggingCorrelationMiddleware(correlationProvider, new NullLogger<LoggingCorrelationMiddleware>(), obtainCorrelationIdFromRequestHeaders: true);

            await correlationMiddleware.InvokeAsync(context, (innerHttpContext) => Task.FromResult(0));

            Assert.AreEqual(correlationId, correlationProvider.GetCorrelationId());
        }

        [Test]
        public async Task InvokeAsync_DoesNotObtainCorrelationByDefault()
        {
            var correlationId = "";
            var context = new DefaultHttpContext();
            var correlationProvider = new CorrelationProvider();
            correlationProvider.SetCorrelationId(correlationId);
            var correlationMiddleware = new LoggingCorrelationMiddleware(correlationProvider, new NullLogger<LoggingCorrelationMiddleware>());

            context.Request.Headers.Add("X-Request-Id", "1234");
            await correlationMiddleware.InvokeAsync(context, (innerHttpContext) => Task.FromResult(0));

            Assert.AreEqual(correlationId, correlationProvider.GetCorrelationId());
        }
    }
}
