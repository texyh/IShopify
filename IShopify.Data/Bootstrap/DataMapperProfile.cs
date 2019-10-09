using AutoMapper;
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
        }
    }
}
