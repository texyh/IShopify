using AutoMapper;
using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using IShopify.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using IShopify.Core.Exceptions;

namespace IShopify.Data.Repositories
{
    internal class CustomerRepository : DataRepository<CustomerEntity, int>, ICustomerRepository
    {
        private readonly IShopifyDbContext _dbContext;
        public CustomerRepository(IShopifyDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerEntity> GetAsync(int id, bool allowNull = false, bool isSummary = false)
        {
            ArgumentGuard.NotDefault(id, nameof(id));
            var query = _dbContext.Customers.AsQueryable();

            if(isSummary) 
            {
                query = query
                    .Select(x => new CustomerEntity { Id = x.Id, FirstName = x.FirstName, LastName =  x.LastName, Email = x.Email});
            }

            var customer = await query.FirstOrDefaultAsync(x => x.Id == id);

            if(!allowNull && customer.IsNull()) 
            {
                throw new ObjectNotFoundException($"No customer with id {id} does not exist");
            }

            return customer;
        }

        public async Task<CustomerEntity> GetCustomerWithAuthProfile(int id, bool allowNull = false)
        {
            ArgumentGuard.NotDefault(id, nameof(id));

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if(customer.IsNull() && !allowNull) 
            {
                throw new ObjectNotFoundException($"No customer with id {id} exists");
            }

            return customer;
        }
    }
}
