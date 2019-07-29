using IShopify.Core.Common.Models;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Data
{
    public interface IProductRepository : IDataRepository<ProductEntity>
    {
        Task<IList<ProductEntity>> Search(ProductQueryModel searchQuery);

        Task<IList<ProductEntity>> GetProductInCategory(int categoryId, PagedQuery query);

        Task<IList<ProductEntity>> GetProductInDepartment(int departmentId, PagedQuery query);

        Task<CategoryEntity> GetProductLocation(int id);

        Task<IList<ReviewEntity>> GetProductReviews(int id);

        Task ReviewProduct(ReviewEntity review);

    }
}
