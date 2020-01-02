using System.Collections.Generic;
using System.Threading.Tasks;
using IShopify.Core.Common;
using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Emails;
using IShopify.Core.Exceptions;
using IShopify.Core.Helpers;

namespace IShopify.Services.Customer
{
    public class CustomerEmailService : ICustomerEmailService
    {
        private readonly ICustomerLookupService _customerLookupService;

        private readonly IEmailSender _emailSender;

        private readonly IUrlService _urlService;

        private readonly ICustomerService _customerService;

        public CustomerEmailService(
            ICustomerLookupService customerLookupService, 
            IEmailSender emailSender, 
            IUrlService urlService,
            ICustomerService customerService)
        {
            _customerLookupService = customerLookupService;
            _emailSender = emailSender;
            _urlService = urlService;
            _customerService = customerService;
        }

        public async Task SendPasswordResetEmailAsync(int id)
        {
            ArgumentGuard.NotDefault(id, nameof(id));

            var user = await _customerLookupService.GetCustomerAsync(id);
            var accesskey =  await _customerService.GetResetPasswordAccesskey(id);
            var model = new PasswordResetEmailModel 
            {
                FirstName = user.FirstName,
                AccesskeyUrl = _urlService.GetPasswordResetUrl(accesskey)
            };

            var recipients = new List<EmailRecipient>
            {
                new EmailRecipient() { EmailAddress = user.Email }
            };

            if(user.IsNull()) 
            {
                throw new ObjectNotFoundException($"No customer with id {id} exists");
            }

            var subject = "Reset Password";

            var mailModel = new EmailMessageModel 
            {
                Subject = subject,
                TemplateType = EmailTemplateType.ResetPassword,
                BodyContentModel = model,
                Recipients = recipients
            };

            await _emailSender.SendAsync(mailModel);
        }
    }
}