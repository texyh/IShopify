using IShopify.Core.Data;
using IShopify.Core.Products;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.DomainServices.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> Get(int id)
        {
            var entity =  await _productRepository.GetAsync(id, true);

            return new Product(); // TODO implement when automapper is added;
        }
    }
}
