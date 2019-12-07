using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core
{
    public class ReviewEntity : IEntity<long>
    {
        public long Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public string Review { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ProductEntity Product { get; set; }

        public virtual CustomerEntity Customer { get; set; }
    }
}
