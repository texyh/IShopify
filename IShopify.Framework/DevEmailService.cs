using System;
using System.Net.Mail;
using System.Threading.Tasks;
using IShopify.Core.Config;
using IShopify.Core.Emails;
using IShopify.Core.Framework.Logging;
using IShopify.Core.Helpers;

namespace IShopify.Framework
{
    public class DevEmailService
    {
        private const int SmtpPort = 25;

        private readonly SmtpClient _smtpClient;

        private readonly ILogger _logger;
        
        private readonly AppSettings _appSettings;

        public DevEmailService(
             SmtpClient smtpClient,
            ILogger logger,
            AppSettings appSettings
        )
        {
            smtpClient.Port = SmtpPort;
            smtpClient.Host = appSettings.MailServer;

            _smtpClient = smtpClient;
            _logger = logger;
            _appSettings = appSettings;
        }

        public Task SendAsync(EmailMessage emailMessage)
        {
            try
            {
                var message = GetMessage(emailMessage);

                _smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return Task.CompletedTask;
        }

        private MailMessage GetMessage(EmailMessage emailMessage)
        {
            var message = new MailMessage();

            message.Subject = emailMessage.Subject;
            message.Body = emailMessage.Body;
            message.From = new MailAddress(_appSettings.SenderAddress, _appSettings.SenderName);
            message.Sender = new MailAddress(_appSettings.SenderAddress, _appSettings.SenderName);
            message.IsBodyHtml = true;

            emailMessage.Recipients.ForEach(recipient => message.To.Add(new MailAddress(recipient.EmailAddress, recipient.Name)));

            if (!emailMessage.Bcc.IsNullOrEmpty())
            {
                emailMessage.Bcc.ForEach(recipient => message.Bcc.Add(new MailAddress(recipient.EmailAddress, recipient.Name)));
            }

            if (!emailMessage.Cc.IsNullOrEmpty())
            {
                emailMessage.Cc.ForEach(recipient => message.CC.Add(new MailAddress(recipient.EmailAddress, recipient.Name)));
            }

            return message;
        }
    }
}