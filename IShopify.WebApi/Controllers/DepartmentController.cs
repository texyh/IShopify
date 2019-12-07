using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.Core.Departments;
using IShopify.Core.Departments.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IShopify.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing departments
    /// </summary>
    [Route("deparments")]
    [ApiController]
    [AllowAnonymous]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        /// <summary>
        /// Contructor for deparmnent controller
        /// </summary>
        /// <param name="departmentService"></param>
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        /// <summary>
        /// Endpoint for getting a department;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<Department> GetAsync(int id)
        {
            return await _departmentService.GetAsync(id);
        }

        /// <summary>
        /// Endpoints for getting all departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IList<Department>> GetAllAsync()
        {
            return await _departmentService.GetAllAsync();
        }

        /// <summary>
        /// Endpoint for creating Department
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> CreateAsync(SaveDepartmentModel model)
        {
            return await _departmentService.CreateAsync(model);
        }

        /// <summary>
        /// Endpoint for updating department
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task UpdateAsync(int id, SaveDepartmentModel model)
        {
            await _departmentService.UpdateAsync(id, model);
        }

        /// <summary>
        /// Endpoint for deleting department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteAsync(int id)
        {
            await _departmentService.DeleteAsync(id);
        }
    } 
}