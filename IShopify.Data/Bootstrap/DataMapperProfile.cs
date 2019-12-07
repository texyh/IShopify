using AutoMapper;
using IShopify.Core.Attributes.Models;
using IShopify.Core.Categories.Models;
using IShopify.Core.Departments;
using IShopify.Core.Orders;
using IShopify.Core.Orders.Models.Entity;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Data.Bootstrap
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {
            CreateMap<ProductEntity, ProductEntity>();

            CreateMap<DepartmentEntity, DepartmentEntity>();

            CreateMap<CategoryEntity, CategoryEntity>();

            CreateMap<AttributeEntity, AttributeEntity>();

            CreateMap<OrderEntity, OrderSummary>();
        }
    }
}
