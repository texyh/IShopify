using AutoMapper;
using IShopify.Core;
using IShopify.Core.Customer.Models;
using IShopify.Core.Helpers;
using IShopify.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.DomainServices.Bootstrap
{
    public class DomainServicesMapperProfile : Profile
    {
        public DomainServicesMapperProfile()
        {
            CreateMap<ProductEntity, Product>();
            CreateMap<CategoryEntity, Category>();
            CreateMap<DepartmentEntity, Department>();
            CreateMap<ReviewEntity, Review>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.Customer.Name));

            CreateMap<SaveCustomerViewModel, CustomerEntity>()
                .ForAllMembers(x => x.Condition(y => !y.IsNull()));

            CreateMap<SaveCustomerAddressViewModel, CustomerEntity>()
                .ForAllMembers(x => x.Condition(y => !y.IsNull()));

            CreateMap<CustomerEntity, Customer>();
        }
    }
}
