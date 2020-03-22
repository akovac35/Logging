using com.github.akovac35.Logging.AspNetCore.Correlation;
using com.github.akovac35.Logging.Correlation;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace com.github.akovac35.Logging.AspNetCore.Tests.Correlation
{
    [TestFixture]
    public class CorrelationIdMiddlewareTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task HasCorrelationIdAsync()
        {
            var correlationId = "1234";
            var context = new DefaultHttpContext();
            var servicesMock = new Mock<IServiceProvider>();
            servicesMock.Setup(sp => sp.GetService(typeof(ICorrelationProvider))).Returns(new CorrelationProvider());
            context.RequestServices = servicesMock.Object;
            context.Request.Headers.Add("X-Request-Id", correlationId);
            
            var middleware = new CorrelationIdMiddleware((innerHttpContext) => Task.FromResult(0));
            await middleware.InvokeAsync(context);

            Assert.AreEqual(correlationId, context.GetCorrelationId());
        }
    }
}
