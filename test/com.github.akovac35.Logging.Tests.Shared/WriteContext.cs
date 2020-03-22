


using Microsoft.Extensions.Logging;
using System;

namespace com.github.akovac35.Logging.Tests.Shared
{
    public class WriteContext
    {
        public LogLevel LogLevel { get; set; }

        public EventId EventId { get; set; }

        public object State { get; set; }

        public Exception Exception { get; set; }

        public Func<object, Exception, string> Formatter { get; set; }

        public object Scope { get; set; }

        public string LoggerName { get; set; }

        public string Message
        {
            get
            {
                return Formatter(State, Exception);
            }
        }
    }
}