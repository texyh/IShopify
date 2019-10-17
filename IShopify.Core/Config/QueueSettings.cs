using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Config
{
    public class QueueSettings
    {
        public string Url { get; set; }

        public string  UserName { get; set; }

        public string Password { get; set; }
        public string QueueNamePrefix { get; set; }
        public ushort PrefetchCount { get; set; }
    }
}
