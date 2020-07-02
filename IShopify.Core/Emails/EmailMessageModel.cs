using System.Collections.Generic;

namespace IShopify.Core.Emails
{
    public class EmailMessageModel
    {
        public string Subject { get; set; }

        public EmailTemplateType TemplateType { get; set; }

        public object BodyContentModel { get; set; }

        public IList<EmailRecipient> Recipients { get; set; }

        public IList<EmailRecipient> Cc { get; set; }

        public IList<EmailRecipient> Bcc { get; set; }

        public IList<EmailAttachment> Attachments { get; set; }
    }
}
