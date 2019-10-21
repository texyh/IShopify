using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IShopify.Core.Config;
using IShopify.Core.Framework;
using IShopify.Core.Framework.Logging;
using Microsoft.Extensions.Options;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace IShopify.Framework.Logging
{
    public class DataBaseLogProvider : BaseLogProvider
    {
        private const string tableName = "logs";
        private readonly AppSettings _appSettings;
        
        public DataBaseLogProvider(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        protected override Serilog.Core.Logger GetLogger()
        {
            var logger = new LoggerConfiguration()
                    .WriteTo.PostgreSQL
                    (_appSettings.LoggingDB, 
                    tableName, 
                    ColumnWriters, 
                    needAutoCreateTable: true, 
                    respectCase: true)
                    .CreateLogger();

            return logger;
        }

        private IDictionary<string, ColumnWriterBase> ColumnWriters => new Dictionary<string, ColumnWriterBase>
        {
            { "MessageTemplate", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
            { "Level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
            { "TimeStamp", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
            { "Properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) }
        };

    }
}
