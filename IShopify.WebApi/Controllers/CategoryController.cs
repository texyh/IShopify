using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.Core.Categories;
using IShopify.Core.Categories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IShopify.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing categories
    /// </summary>
    [Route("categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Constructor for Category 
        /// </summary>
        /// <param name="categoryService"></param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Endpoint for getting all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IList<Category>> GetAllAsync()
        {
            return await _categoryService.GetAllAsync();
        }

        /// <summary>
        /// Endpoint for getting a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<Category> GetAsync(int id)
        {
            return await _categoryService.GetAsync(id);
        }

        /// <summary>
        /// Endpoint for creating category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> CreateAsync(SaveCategoryModel model)
        {
            return await  _categoryService.AddAsync(model);
        }

        /// <summary>
        /// Endpoint for delete category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteAsync(int id)
        {
            await _categoryService.DeleteAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task UpdateAsync(int id, SaveCategoryModel model)
        {
            await _categoryService.UpdateAsync(id, model);
        }
    }
}