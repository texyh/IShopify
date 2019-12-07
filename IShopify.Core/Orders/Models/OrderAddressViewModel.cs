using IShopify.Core.Customer.Models;

namespace IShopify.Core.Orders
{
    public class OrderAddressViewModel: Address
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}