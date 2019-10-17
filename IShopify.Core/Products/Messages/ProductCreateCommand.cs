using IShopify.Core.MessageBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Messages
{
    public class ProductCreateCommand : Command
    {
        public ProductCreateCommand(int userId, int productId) : base(userId)
        {
            ProductId = productId;
        }

        public int ProductId { get; set;  }
    }
}
