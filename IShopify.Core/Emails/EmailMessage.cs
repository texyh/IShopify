using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Emails
{
    public class EmailMessage
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public IList<EmailRecipient> Recipients { get; set; }

        public IList<EmailRecipient> Cc { get; set; }

        public IList<EmailRecipient> Bcc { get; set; }

        public IList<EmailAttachment> Attachments { get; set; }

        public void EmailAddress()
        {
            Recipients = new List<EmailRecipient>();
            Cc = new List<EmailRecipient>();
            Bcc = new List<EmailRecipient>();
            Attachments = new List<EmailAttachment>();
        }
    }
}
