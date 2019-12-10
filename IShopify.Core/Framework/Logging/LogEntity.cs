using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Framework.Logging {
    public class LogEntity {
        public string Id => Properties?.LogEntry?.LogId.ToString ();
        public string Message { get; set; }

        public string Exception { get; set; }

        public DateTime TimeStamp { get; set; }

        public string MessageTemplate { get; set; }

        public LogProperties Properties { get; set; }

    }

    public class LogProperties {
        public LogEntryValues LogEntry { get; set; }
    }

    public class LogEntryValues {
        public Guid LogId { get; set; }

        public string Message { get; set; }

        public string ExceptionType { get; set; }

        public string BaseExceptionType { get; set; }

        public string User { get; set; }

        public string Logger { get; set; }

        public string Exception { get; set; }
    }

}