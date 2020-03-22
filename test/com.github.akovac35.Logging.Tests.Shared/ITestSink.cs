using System;
using System.Collections.Concurrent;

namespace com.github.akovac35.Logging.Tests.Shared
{
    public interface ITestSink
    {
        event Action<WriteContext> MessageLogged;

        event Action<BeginScopeContext> ScopeStarted;

        Func<WriteContext, bool> WriteEnabled { get; set; }

        Func<BeginScopeContext, bool> BeginEnabled { get; set; }

        IProducerConsumerCollection<BeginScopeContext> Scopes { get; set; }

        IProducerConsumerCollection<WriteContext> Writes { get; set; }

        void Write(WriteContext context);

        void Begin(BeginScopeContext context);
    }
}
