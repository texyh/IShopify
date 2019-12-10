using System.Threading.Tasks;

namespace IShopify.Core.Emails
{
    public interface IEmailSender
    {
         Task SendAsync(EmailMessageModel messageModel);
    }
}