using AutoMapper;
using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Data.Repositories
{
    internal class CustomerRepository : DataRepository<CustomerEntity>, ICustomerRepository
    {
        private readonly IShopifyDbContext _dbContext;
        public CustomerRepository(IShopifyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }
    }
}
