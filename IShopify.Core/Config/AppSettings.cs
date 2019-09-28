using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Config
{
    public class AppSettings
    {
        public string TokenKey { get; set; }

        public string BaseUrl { get; set; }

        public string AppName { get; set; }

        public string Salt { get; set; }

        public bool SendErrorDetails { get; set; } = false;

        public string LoggingDB {get; set;}

    }
}
