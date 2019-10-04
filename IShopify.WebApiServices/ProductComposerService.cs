using AutoMapper;
using IShopify.Core.Common.Models;
using IShopify.Core.Products;
using IShopify.Core.Products.Models;
using IShopify.WebApiServices.ViewModels;
using IShopify.WebApiServices.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.WebApiServices
{
    public class ProductComposerService : IProductComposerService
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductComposerService(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductSummaryViewModel>> GetProductInCategory(int categoryId, PagedQuery query)
        {
            var result = await _productService.GetProductInCategoryAsync(categoryId, query);
            return ToProductSummaries(result, query);
        }

        public async Task<PagedResult<ProductSummaryViewModel>> GetProductInDepartment(int departmentId, PagedQuery query)
        {
            var result = await _productService.GetProductInDepartmentAsync(departmentId, query);
            return ToProductSummaries(result, query);
        }

        public async Task<ProductLocationViewModel> GetProductLocation(int id)
        {
            var result = await _productService.GetProductLocationAsync(id);
            return new ProductLocationViewModel
            {
                CategoryId = result.Id,
                CategoryName = result.Name,
                DepartmentId = result.DepartmentId,
                DepartmentName = result.Department.Name
            };
        }

        //public Task<IList<ReviewViewModel>> GetProductReviews(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<PagedResult<ProductSummaryViewModel>> Search(ProductQueryModel query)
        {
            var result = await _productService.SearchAsync(query);
            return ToProductSummaries(result, new PagedQuery { PageNumber = query.PageNumber, PageSize = query.PageSize });
        }

        private PagedResult<ProductSummaryViewModel> ToProductSummaries(IList<Product> products, PagedQuery query)
        {
            var summaries = _mapper.Map<IList<Product>, IList<ProductSummaryViewModel>>(products);

            return new PagedResult<ProductSummaryViewModel>
            {
                Items = summaries,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

    }

    public interface IProductComposerService
    {
        Task<PagedResult<ProductSummaryViewModel>> Search(ProductQueryModel query);

        Task<PagedResult<ProductSummaryViewModel>> GetProductInCategory(int categoryId, PagedQuery query);

        Task<PagedResult<ProductSummaryViewModel>> GetProductInDepartment(int departmentId, PagedQuery query);

        Task<ProductLocationViewModel> GetProductLocation( int id);

        //Task<IList<ReviewViewModel>> GetProductReviews(int id);

    }
}
