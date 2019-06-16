using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Products
{
    public interface IProductService
    {
        Task<Product> Get(int id);
    }
}
