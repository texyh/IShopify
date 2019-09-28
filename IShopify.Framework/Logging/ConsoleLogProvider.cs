using IShopify.Core.Framework.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Framework.Logging
{
    public class ConsoleLogProvider : BaseLogProvider
    {
        public ConsoleLogProvider()
        {
        }

        protected override Serilog.Core.Logger GetLogger()
        {
            return new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }
    }
}

