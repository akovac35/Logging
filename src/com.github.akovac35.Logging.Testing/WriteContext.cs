// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač
//   Microsoft

using Microsoft.Extensions.Logging;
using System;

namespace com.github.akovac35.Logging.Testing
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class WriteContext
    {
        public LogLevel LogLevel { get; set; }

        public EventId EventId { get; set; }

        public object State { get; set; }

        public Exception Exception { get; set; }

        public Func<object, Exception, string> Formatter { get; set; }

        public ScopeContext Scope { get; set; }

        public ILogger Logger { get; set; }

        public DateTime Timestamp { get; set; }

        public int ThreadId { get; set; }

        public virtual string Message
        {
            get
            {
                return Formatter(State, Exception);
            }
        }

        public override string ToString()
        {
            return $"[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] {ThreadId} {LogLevel} EventId: {EventId}{(Scope != null ? $" Scope: <{Scope}>" : "")} Message: {Message}{(Exception != null ? $"{Environment.NewLine}{Exception}" : "")}";
        }
    }
}
