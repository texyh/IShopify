using AutoMapper;
using IShopify.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Data.Repositories
{
    internal class AccessKeyRepository : DataRepository<AccessKeyEntity, Guid>, IAccessKeyRepository
    {
        public AccessKeyRepository(
            IShopifyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
