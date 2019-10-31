using IShopify.Core.Attributes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Data
{
    public interface IAttributeRepository : IDataRepository<AttributeEntity, int>
    {
        Task<int> AddValueAsync(AttributeValueEntity value);
    }
}
