using AutoMapper;
using IShopify.Core.Categories.Models;
using IShopify.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Data.Repositories
{
    internal class CategoryRepository : DataRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(
            IShopifyDbContext dbContext,
            IMapper mapper) : base(dbContext, mapper)
        { }
    }
}
