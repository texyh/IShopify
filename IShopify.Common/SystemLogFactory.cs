using IShopify.Core.Framework.Logging;
using IShopify.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Common
{
    public static class SystemLogFactory
    {
        public static ILogger Create()
        {
            var systemId = int.MaxValue;
            var systemName = "IShopify";
            var systemEmail = "Ishopify@email.com";

            var logProvider = AppSettingsProvider.Current.LogTarget == LogTarget.Console ?
                               new ConsoleLogProvider() as ILogProvider :
                               new DataBaseLogProvider(AppSettingsProvider.Current);

            var userContext = UserContext.Create(systemId, systemEmail, systemName);
            return new Logger(logProvider, userContext);
        }
    }
}
