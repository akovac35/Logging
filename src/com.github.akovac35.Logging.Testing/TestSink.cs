// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač
//   Microsoft

using System;
using System.Collections.Concurrent;
using System.Threading;

namespace com.github.akovac35.Logging.Testing
{
    public class TestSink : ITestSink, IDisposable
    {
        // current, parent
        protected ConcurrentBag<ScopeContext> _scopes;

        protected ConcurrentBag<WriteContext> _writes;

        public TestSink(
            Func<WriteContext, bool> writeEnabled = null,
            Func<ScopeContext, bool> beginEnabled = null)
        {
            WriteEnabled = writeEnabled;
            BeginEnabled = beginEnabled;

            _scopes = new ConcurrentBag<ScopeContext>();
            _writes = new ConcurrentBag<WriteContext>();
        }

        public virtual Func<WriteContext, bool> WriteEnabled { get; set; }

        public virtual Func<ScopeContext, bool> BeginEnabled { get; set; }

        public virtual ConcurrentBag<ScopeContext> Scopes { get => _scopes; }

        public virtual ConcurrentBag<WriteContext> Writes { get => _writes;  }

        protected AsyncLocal<ScopeContext> _currentScope = new AsyncLocal<ScopeContext>();
        public virtual ScopeContext CurrentScope 
        {
            get
            {
                return _currentScope.Value;
            }
            set
            {
                _currentScope.Value = value;
            }
        }

        public event Action<WriteContext> MessageLogged;

        public event Action<ScopeContext> ScopeStarted;

        public virtual void Write(WriteContext context)
        {
            if (WriteEnabled == null || WriteEnabled(context))
            {
                context.Scope = CurrentScope;
                _writes.Add(context);
                MessageLogged?.Invoke(context);
            }
        }

        public virtual void Begin(ScopeContext context)
        {
            if (BeginEnabled == null || BeginEnabled(context))
            {
                context.ParentScope = CurrentScope;
                context.Sink = this;
                CurrentScope = context;
                _scopes.Add(context);
                ScopeStarted?.Invoke(context);
            }
        }

        public virtual void Clear()
        {
            foreach (var item in _scopes)
            {
                // Avoid post-clear modification of this sink
                item?.Dispose();
            }
            
            _scopes.Clear();
            _writes.Clear();
        }

        private bool _disposedValue;

        public virtual void Dispose()
        {
            if (!_disposedValue)
            {
                Clear();

                _disposedValue = true;
            }
        }
    }
}
