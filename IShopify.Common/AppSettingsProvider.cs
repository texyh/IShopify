using IShopify.Core.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Common
{
    public static class AppSettingsProvider
    {
        public static AppSettings Current { get; set; }

        public static void Register(AppSettings appSettings)
        {
            Current = appSettings;
        }
    }
}
