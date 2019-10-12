namespace IShopify.Core.Config
{
    public class RedisSettings
    {
        public string Host { get; set; }

        public StackExchange.Redis.ConfigurationOptions Options {get; set;}

        public string Instance => "IShopifyInstance";
    }
}