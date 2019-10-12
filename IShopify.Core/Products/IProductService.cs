using IShopify.Core.Common.Models;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Products
{
    public interface IProductService
    {
        Task<Product> GetAsync(int id);

        Task<IList<Product>> SearchAsync(ProductQueryModel query);

        Task<IList<Product>> GetProductInCategoryAsync(int categoryId, PagedQuery query);

        Task<IList<Product>> GetProductInDepartmentAsync(int departmentId, PagedQuery query);

        Task<Category> GetProductLocationAsync(int id);

        Task<IList<Review>> GetProductReviewsAsync(int id);

        Task ReviewProductAsync(int id, string review, int rating);

        Task<int> AddProductAsync(SaveProductModel model);

        Task UpdateProductAsync(int id, SaveProductModel model);

    }
}
