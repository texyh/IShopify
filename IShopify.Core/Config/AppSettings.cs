using IShopify.Core.Framework.Logging;
using IShopify.Core.Helpers;
using Microsoft.Extensions.Configuration;
using Npgsql;
using StackExchange.Redis;
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

        public string LoggingDB => IshopifyDB;

        public string IshopifyDB => GetDbUrl("DATABASE_URL");

        public RedisSettings RedisSettings => GetRedisSettings("REDIS_URL");

        public QueueSettings QueueSettings => GetQueueSettings("CLOUDAMQP_URL");

        public LogTarget LogTarget => (LogTarget)Convert.ToInt32(GetValue("LogTarget"));

        public string MailServer =>  GetValue("MailServer");

        public string SenderAddress =>  GetValue("SenderAddress");

        public string SenderName =>  GetValue("SenderName");

        public string WebUrl => "http://localhost:4200"; // TODO move to env

        public string GetValue(string key, string defaultValue = null)
        {
            return Environment.GetEnvironmentVariable(key)?.Trim() ?? _configuration[key]?.Trim() ?? defaultValue;
        }

        private string GetDbUrl(string dbkey)
        {
            var url = GetValue(dbkey);

            if(isProduction)
            {
                return ParsePostgresUrl(url);
            }

            return url;
        }

        private string ParsePostgresUrl(string dburl)
        {
            bool isUrl = Uri.TryCreate(dburl, UriKind.Absolute, out var url);
            if (isUrl)
            {
                var userInfo = url.UserInfo.Split(':');
                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = url.Host,
                    Username = userInfo[0],
                    Password = userInfo[1],
                    Database = url.LocalPath.Substring(1),
                    SslMode = SslMode.Prefer,
                    TrustServerCertificate = true
                };

                Console.WriteLine(builder.ToString());
                return builder.ToString();
            }

            throw new InvalidOperationException("This is not a valid production postgres url");
        }

        private RedisSettings GetRedisSettings(string key) 
        {
            var url = GetValue(key);

            if(isProduction) 
            {
                var isUrl = Uri.TryCreate(url, UriKind.Absolute, out var parsedUrl);

                if(isUrl) 
                {
                    var userInfo = parsedUrl.UserInfo.Split(':');
                    var configOptions = new ConfigurationOptions
                    {
                        ClientName = userInfo[0],
                        Password = userInfo[1],
                    };

                    configOptions.EndPoints.Add($"{parsedUrl.Host}:{parsedUrl.Port}");

                    return new RedisSettings 
                    {
                        Options = configOptions
                    };
                }
            }

            return new RedisSettings 
            {
                Host = url
            };
        }

        private QueueSettings GetQueueSettings(string key)
        {
            if(isProduction)
            {
                var value = GetValue(key);
                var isValidUrl = Uri.TryCreate(value, UriKind.Absolute, out var url);

                if(isValidUrl)
                {
                    var userInfo = url.UserInfo.Split(':');
                    var queueSettings = new QueueSettings
                    {
                        Url = $"rabbitmq://{url.Host}/{userInfo[0]}",
                        QueueNamePrefix = "prod_ishopify_",
                        PrefetchCount = 100,
                        Password = userInfo[1],
                        UserName = userInfo[0]
                    };

                    return queueSettings;
                }
                
            }

            return new QueueSettings
            {
                Url = "rabbitmq://localhost",
                UserName = "guest",
                Password = "guest",
                QueueNamePrefix = "prod_ishopify_",
                PrefetchCount = 100
            };
        }

        public static bool isProduction => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
    }
}
