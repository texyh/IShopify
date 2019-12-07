using IShopify.Core.Categories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Categories
{
    public interface ICategoryService
    {
        Task<Category> GetAsync(int id);

        Task<IList<Category>> GetAllAsync();

        Task<int> AddAsync(SaveCategoryModel model);

        Task UpdateAsync(int id, SaveCategoryModel model);

        Task DeleteAsync(int id);
    }
}
