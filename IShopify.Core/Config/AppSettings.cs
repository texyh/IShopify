using IShopify.Core.Framework.Logging;
using IShopify.Core.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Config
{
    public class AppSettings
    {
        private readonly IConfiguration _configuration;
        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TokenKey => GetValue("TokenKey");

        public string BaseUrl => GetValue("BaseUrl");

        public string AppName => GetValue("AppName");

        public string Salt => GetValue("Salt");

        public bool SendErrorDetails  => GetValue("SendErrorDetails", "false").ToBool();

        public string LoggingDB => GetValue("LoggingDB");

        public string IshopifyDB => GetValue("IshopifyDB");

        public LogTarget LogTarget => (LogTarget)Convert.ToInt32(GetValue("LogTarget"));

        public string GetValue(string key, string defaultValue = null)
        {
            return Environment.GetEnvironmentVariable(key)?.Trim() ?? _configuration[key]?.Trim() ?? defaultValue;
        }
    }
}
