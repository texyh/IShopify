using IShopify.Core.Categories.Models;
using IShopify.Core.Common.Models;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Data
{
    public interface IProductRepository : IDataRepository<ProductEntity>, IDisposable
    {
        Task<IList<ProductEntity>> SearchAsync(ProductQueryModel searchQuery);

        Task<IList<ProductEntity>> GetProductInCategoryAsync(int categoryId, PagedQuery query);

        Task<IList<ProductEntity>> GetProductInDepartmentAsync(int departmentId, PagedQuery query);

        Task<CategoryEntity> GetProductLocationAsync(int id);

        Task<IList<ReviewEntity>> GetProductReviewsAsync(int id);

        Task ReviewProductAsync(ReviewEntity review);

    }
}
