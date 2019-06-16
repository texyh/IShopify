using IShopify.Core.Data;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Data.Repositories
{
    internal class ProductRepository : DataRepository<ProductEntity>, IProductRepository
    {
        private readonly IShopifyDbContext _dbContext;
        public ProductRepository(IShopifyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
