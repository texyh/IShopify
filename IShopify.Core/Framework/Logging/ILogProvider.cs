using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Framework.Logging {
    public interface ILogProvider {
        Guid LogEvent (LogLevel logLevel, LogEntryValues logEntity);
    }
}