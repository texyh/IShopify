using IShopify.Core.Framework.Logging;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Framework.Logging
{
    public class BaseLogProvider : ILogProvider
    {
        private readonly Lazy<Serilog.Core.Logger> _logger;
        private const string LogEntryTemplate = "{@LogEntry}";

        public BaseLogProvider()
        {
            _logger = new Lazy<Serilog.Core.Logger>(GetLogger, true);
        }

        public Guid LogEvent(LogLevel logLevel, LogEntryValues logEntry)
        {
            switch ((LogEventLevel)logLevel)
            {
                case LogEventLevel.Debug:
                    _logger.Value.Debug(LogEntryTemplate, logEntry);
                    break;

                case LogEventLevel.Error:
                    _logger.Value.Error(LogEntryTemplate, logEntry);
                    break;

                case LogEventLevel.Information:
                    _logger.Value.Information(LogEntryTemplate, logEntry);
                    break;

                case LogEventLevel.Verbose:
                    _logger.Value.Verbose(LogEntryTemplate, logEntry);
                    break;

                case LogEventLevel.Fatal:
                    _logger.Value.Fatal(LogEntryTemplate, logEntry);
                    break;

                case LogEventLevel.Warning:
                    _logger.Value.Warning(LogEntryTemplate, logEntry);
                    break;
            }

            return logEntry.LogId;
        }

        protected virtual Serilog.Core.Logger GetLogger()
        {
            throw new NotImplementedException();
        }
    }
}
