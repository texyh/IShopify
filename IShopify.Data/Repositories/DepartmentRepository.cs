using AutoMapper;
using IShopify.Core.Data;
using IShopify.Core.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Data.Repositories
{
    internal class DepartmentRepository : DataRepository<DepartmentEntity>, IDepartmentRepository
    {
        public DepartmentRepository(
            IShopifyDbContext shopifyDbContext,
            IMapper mapper) 
            : base(shopifyDbContext, mapper)
        { }
    }
}
