using IShopify.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Orders.Models
{
    public class CreditCardPaymentInformationEntity : IEntity<int>
    {
        public int Id => throw new NotImplementedException();

        public DateTime? DeleteDateUtc {get; set;}

    }
}
