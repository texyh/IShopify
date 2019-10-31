using AutoMapper;
using IShopify.Core.Attributes.Models;
using IShopify.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Data.Repositories
{
    internal class AttributeRepository : DataRepository<AttributeEntity, int>, IAttributeRepository
    {
        private readonly IShopifyDbContext _dbContext;

        public AttributeRepository(
            IShopifyDbContext dbcontext,
            IMapper mapper) : base(dbcontext, mapper)
        {
            _dbContext = dbcontext;
        }

        public Task<int> AddValueAsync(AttributeValueEntity value)
        {
            _dbContext.AttributeValues.Add(value);

            return _dbContext.SaveChangesAsync();
        }
    }
}
