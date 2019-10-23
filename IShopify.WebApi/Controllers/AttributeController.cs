using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IShopify.Core.Attributes;
using IShopify.Core.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IShopify.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("attributes")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService _attributeService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeService"></param>
        public AttributeController(
            IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> CreateAsync(string attribute)
        {
            return await _attributeService.AddAsync(attribute);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/details")]
        public async Task<NamedId<int>> GetAsync(int id)
        {
            return await _attributeService.GetAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IList<NamedId<int>>> GetAllAsync()
        {
            return await _attributeService.GetAllAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task UpdateAsync(int id, string attribute)
        {
            await _attributeService.UpdateAsync(id, attribute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteAsync(int id)
        {
            await _attributeService.DeleteAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        [HttpGet("{id}/values")]
        public async Task<IList<NamedId<int>>> GetAllValuesAsync(int id)
        {
            return await _attributeService.GetValuesAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("{id}/values")]
        public async Task<int> CreateValueAsync(int id, string value)
        {
            return await _attributeService.AddValueAsync(id, value);
        }
    }
}