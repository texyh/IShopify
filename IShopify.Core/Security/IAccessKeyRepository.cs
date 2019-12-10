using System;
using IShopify.Core.Data;

namespace IShopify.Core.Security
{
    public interface IAccessKeyRepository : IDataRepository<AccessKeyEntity, Guid>
    {
         
    }
}