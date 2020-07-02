using IShopify.Core.MessageBus;

namespace IShopify.Core.Customer
{
    public class PasswordResetRequestCommand : Command
    {
        public PasswordResetRequestCommand(int userId, string email) : base(userId)
        {
            Email = email;
        }

        public string Email {get; set;}
    }
}