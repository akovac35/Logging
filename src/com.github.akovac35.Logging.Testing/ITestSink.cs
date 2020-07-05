// Au// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač
//   Microsoft

using System;
using System.Collections.Concurrent;

namespace com.github.akovac35.Logging.Testing
{
    public interface ITestSink
    {
        event Action<WriteContext> MessageLogged;

        event Action<ScopeContext> ScopeStarted;

        Func<WriteContext, bool> WriteEnabled { get; set; }

        Func<ScopeContext, bool> BeginEnabled { get; set; }

        ConcurrentBag<ScopeContext> Scopes { get; }

        ConcurrentBag<WriteContext> Writes { get; }

        void Write(WriteContext context);

        void Begin(ScopeContext context);

        void Clear();

        ScopeContext CurrentScope { get; set; }
    }
}
