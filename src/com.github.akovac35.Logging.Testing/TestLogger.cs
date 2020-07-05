// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač
//   Microsoft

using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace com.github.akovac35.Logging.Testing
{
    public class TestLogger : ILogger
    {
        public TestLogger(string name, ITestSink sink)
            : this(name, sink, _ => true)
        {
        }

        public TestLogger(string name, ITestSink sink, Func<LogLevel, bool> filter)
        {
            _sink = sink ?? throw new ArgumentNullException(nameof(sink));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public virtual string Name { get; protected set; }

        protected ITestSink _sink;

        protected string _name;

        protected Func<LogLevel, bool> _filter;

        public virtual IDisposable BeginScope<TState>(TState state)
        {
            var ctx = NewScopeContext();
            ctx.Logger = this;
            ctx.State = state;

            _sink.Begin(ctx);
            return ctx;
        }

        public virtual void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var ctx = NewWriteContext();
            ctx.LogLevel = logLevel;
            ctx.EventId = eventId;
            ctx.State = state;
            ctx.Exception = exception;
            ctx.Formatter = (s, e) => formatter((TState)s, e);
            ctx.Logger = this;
            ctx.Timestamp = DateTime.Now;
            ctx.ThreadId = Thread.CurrentThread.ManagedThreadId;

            _sink.Write(ctx);
        }

        public virtual bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None && _filter(logLevel);
        }

        protected virtual WriteContext NewWriteContext()
        {
            return new WriteContext();
        }

        protected virtual ScopeContext NewScopeContext()
        {
            return new ScopeContext();
        }
    }
}
