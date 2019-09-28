﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IShopify.Common;
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
    class LogDbProvider : BaseLogProvider
    {
        private const string tableName = "logs";
        
        public LogDbProvider()
        {
        }

        protected override Serilog.Core.Logger GetLogger()
        {
            var logger = new LoggerConfiguration()
                    .WriteTo.PostgreSQL
                    (AppSettingsProvider.Current.LoggingDB, 
                    tableName, 
                    ColumnWriters, 
                    needAutoCreateTable: true, 
                    respectCase: true)
                    .CreateLogger();

            return logger;
        }

        private IDictionary<string, ColumnWriterBase> ColumnWriters => new Dictionary<string, ColumnWriterBase>
        {
            { "Message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
            { "MessageTemplate", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
            { "Level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
            { "TimeStamp", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
            { "Properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) }
        };

    }
}