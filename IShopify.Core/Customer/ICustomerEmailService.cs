using System.Threading.Tasks;

namespace IShopify.Core.Customer
{
    public interface ICustomerEmailService
    {
         Task SendPasswordResetEmailAsync(int id);
    }
}