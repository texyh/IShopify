using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Emails
{
    public class EmailAttachment
    {
        public string FileName { get; set; }

        public byte[] Content { get; set; }
    }
}
