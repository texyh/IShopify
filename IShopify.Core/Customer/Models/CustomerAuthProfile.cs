namespace IShopify.Core.Customer.Models
{
    public class CustomerAuthProfile
    {
        public string  Password { get; set; }

        public bool EmailVerified {get; set;}

        public string Salt {get; set;}
    }
}