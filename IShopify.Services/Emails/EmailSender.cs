using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IShopify.Core.Common;
using IShopify.Core.Config;
using IShopify.Core.Emails;
using IShopify.Core.Framework;
using IShopify.Core.Framework.Logging;
using IShopify.Core.Helpers;

namespace IShopify.Services.Emails
{
    public class EmailSender : IEmailSender
    {
        private readonly ITemplateService _templateService;

        private readonly ITemplateLoader _templateLoader;
        
        private readonly IEmailService _emailService;

        private readonly AppSettings _appSettings;

        private readonly ILogger _logger;

        private readonly string EmailLayoutKey = $"EmailLayout::Layout1";

        public EmailSender(
            IEmailService emailService,
            AppSettings appSettings,
            ILogger logger,
            ITemplateService templateService,
            ITemplateLoader templateLoader)
        {
            _emailService = emailService;
            _appSettings = appSettings;
            _logger = logger;
            _templateService = templateService;
            _templateLoader = templateLoader;
        }

        public async Task SendAsync(EmailMessageModel messageModel)
        {
            try
            {
                await DoSendAsync(messageModel);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private async Task DoSendAsync(EmailMessageModel messageModel)
        {
            var emailBody = await GetEmailBody(messageModel.TemplateType, messageModel.BodyContentModel);

            var emailMessage = new EmailMessage
            {
                Subject = messageModel.Subject,
                Attachments = messageModel.Attachments,
                Recipients = messageModel.Recipients,
                Cc = messageModel.Cc,
                Bcc = messageModel.Bcc,
                Body = emailBody
            };

            await _emailService.SendAsync(emailMessage);
        }

        private async Task<string> GetEmailBody<T>(EmailTemplateType templateType, T emailModel)
        {
            var templateKey = GetTemplateKey(templateType);
            var appName = _appSettings.AppName;
            var viewBag = new Dictionary<string, object>
            {
                { "AppName", appName }
            };

            if (_templateService.IsTemplateCached(templateKey))
            {
                return _templateService.Execute(templateKey, emailModel, viewBag);
            }

            var template = await _templateLoader.LoadEmailTemplateAsync(templateType);
            var layout = await _templateLoader.LoadEmailTemplateAsync(EmailTemplateType.EmailLayout);

            return _templateService.Execute(templateKey, emailModel, template, EmailLayoutKey, layout, viewBag);
        }

        private string GetTemplateKey(EmailTemplateType templateType)
        {
            return $"EmailBodyTemplate::{templateType}";
        }
    }
}