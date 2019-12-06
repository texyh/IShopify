using AutoMapper;
using IShopify.Core.Data;
using IShopify.Core.Exceptions;
using IShopify.Core.Helpers;
using IShopify.Core.Orders;
using IShopify.Core.Orders.Models;
using IShopify.Core.Orders.Models.Entities;
using IShopify.Core.Orders.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Data.Repositories
{
    internal class OrderRepository : DataRepository<OrderEntity, Guid>, IOrderRepository
    {
        private IShopifyDbContext _dbContext;
        private IMapper _mapper;

        public OrderRepository(
            IShopifyDbContext dbContext,
            IMapper mapper): base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task CreateOrder(OrderEntity order, IList<OrderItemEntity> orderItems)
        {
            _dbContext.Orders.Add(order);
            _dbContext.OrderItems.AddRange(orderItems);

            return _dbContext.SaveChangesAsync();
        }

        public async Task<IList<AddressEntity>> GetAllCustomerAddressAsync(int customerId, bool isBilling)
        {
            return await _dbContext.Addresses
                .Where(x => x.CustomerId == customerId && x.IsBillingAddress == isBilling)
                .ToListAsync();
        }

        public async Task<OrderEntity> GetAsync(Guid id, bool allowNull = false, bool isSummary = false)
        {
            ArgumentGuard.NotDefault(id, nameof(id));

            var query = _dbContext.Orders.AsQueryable();
            
            if(isSummary) 
            {
                query = query
                    .Select(x => new OrderEntity {Id = x.Id, ShippingAddressId = x.ShippingAddressId});
            }

            var order = await query.FirstOrDefaultAsync(x => x.Id == id);

            if(!allowNull && order.IsNull()) 
            {
                throw new ObjectNotFoundException($"No order with id {id} does not exist");
            }

            return order;
        }

        public async Task SaveOrderAddressAsync(AddressEntity model)
        {
            ArgumentGuard.NotNull(model, nameof(model));

            _dbContext.Addresses.Add(model);

            await _dbContext.SaveChangesAsync();
        }
    }
}
