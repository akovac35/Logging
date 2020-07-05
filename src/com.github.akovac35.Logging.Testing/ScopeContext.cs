// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač
//   Microsoft

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.github.akovac35.Logging.Testing
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class ScopeContext : IDisposable
    {
        public object State { get; set; }

        /// <summary>
        /// KV version of State.
        /// </summary>
        public KeyValuePair<string, object>[] KvState
        {
            get
            {
                return State as KeyValuePair<string, object>[];
            }
        }

        public ILogger Logger { get; set; }

        public ScopeContext ParentScope {get; set; }

        public ITestSink Sink { get; set; }

        private bool _disposedValue;

        public virtual void Dispose()
        {
            if (!_disposedValue)
            {
                if(Sink != null) Sink.CurrentScope = ParentScope;
                Sink = null;
                _disposedValue = true;
            }
        }

        public override string ToString()
        {
            return $"State: {(KvState != null ? ToString(KvState) : State)}{(ParentScope != null ? $" ParentScope: <{ParentScope}>" : "")}";
        }

        protected virtual string ToString(KeyValuePair<string, object>[] keyValuePairs)
        {
            if (keyValuePairs == null) return null;

            string result = "[";
            result += String.Join("", keyValuePairs.Select(item => $"{{{item.Key}, {item.Value}}}"));
            result += "]";

            return result;
        }
    }
}
