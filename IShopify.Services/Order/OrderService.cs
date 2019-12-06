using AutoMapper;
using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using IShopify.Core.Helpers;
using IShopify.Core.MessageBus;
using IShopify.Core.Orders;
using IShopify.Core.Orders.Messages;
using IShopify.Core.Orders.Models;
using IShopify.Core.Orders.Models.Entity;
using IShopify.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models = IShopify.Core.Orders.Models;

namespace IShopify.DomainServices.Order
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserContext _userContext;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IMessageBus _bus;

        public OrderService(IOrderRepository orderRepository,
            IProductRepository productRepository,
            IUserContext userContext,
            ICustomerRepository customerRepository, 
            IMapper mapper,
            IMessageBus bus)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userContext = userContext;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task ConfirmOrder(Guid id)
        {
            var order = await _orderRepository.GetAsync(id, isSummary: true);

            //TODO add permissions to confirm order

            order.OrderStatus = OrderStatus.Confirmed;

            await _orderRepository.UpdateFieldsAsync(order, nameof(order.OrderStatus));

            await _bus.PublishAsync(new OrderConfirmedCommand(_userContext.UserId, id));
        }

        public async Task<Guid> CreateAsync(IList<SaveOrderItemViewModel> orderItems)
        {
            ArgumentGuard.NotNull(orderItems, nameof(orderItems));

            var products = await _productRepository
                .GetProductSummaries(orderItems.Select(x => x.ProductId).ToList())
                .ContinueWith(task => task.Result.ToDictionary(x => x.Id));

            var items = orderItems.Select(x =>
                new OrderItem
                { 
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitCost = products[x.ProductId].Price,
                });

            // TODO validate orderitems
            // TODO validate order


            var order = new OrderEntity
            {
                Id = Guid.NewGuid(),
                TotalAmount = items.Sum(x => x.SubTotal),
                CreatedOn = DateTime.UtcNow,
                CustomerId = _userContext.UserId,
                PaymentStatus = PaymentStatus.Pending,
                OrderStatus = OrderStatus.Pending,
            };

            var orderItemsentity = items.Select(x => ToOrderItemEntity(x, order.Id)).ToList();

            await _orderRepository.CreateOrder(order, orderItemsentity);

            return order.Id;

        }

        private OrderItemEntity ToOrderItemEntity(OrderItem order, Guid orderId)
        {
            return new OrderItemEntity
            {
                OrderId = orderId,
                ProductId = order.ProductId,
                Quantity = order.Quantity
            };
        }
    }
}
