using StackExchange.Redis;

namespace IShopify.Core.Config
{
    public class RedisSettings
    {
        public string Host { get; set; }

        public ConfigurationOptions Options {get; set;}

        public string Instance => "IShopifyInstance";
    }
}