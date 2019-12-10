using System.Threading.Tasks;

namespace IShopify.Core.Emails
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage emailMessage);
    }
}