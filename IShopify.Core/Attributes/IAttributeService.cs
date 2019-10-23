using IShopify.Core.Attributes.Models;
using IShopify.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Attributes
{
    public interface IAttributeService
    {
        Task<int> AddAsync(string attribute);

        Task UpdateAsync(int id, string attribute);

        Task<NamedId<int>> GetAsync(int id);

        Task<IList<NamedId<int>>> GetAllAsync();

        Task DeleteAsync(int id);

        Task<int> AddValueAsync(int attributeId, string value);

        Task UpdateValueAsync(int id, int attributeId, string value);

        Task<IList<NamedId<int>>> GetValuesAsync(int attributeId);

        Task DeleteValueAsync(int valueId);
    }
}
