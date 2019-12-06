using AutoMapper;
using IShopify.Core.Data;
using IShopify.Core.Helpers;
using IShopify.Core.Orders;
using IShopify.Core.Orders.Models;
using IShopify.Core.Orders.Models.Entities;
using IShopify.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.DomainServices.Shipping
{
    public class ShippingService : IShippingService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public ShippingService(
            IOrderRepository orderRepository,
            IMapper mapper,
            IUserContext userContext)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<IList<OrderAddressViewModel>> GetAllCustomerShippingAddressAsync()
        {
            var addresses = await _orderRepository.GetAllCustomerAddressAsync(_userContext.UserId);

            return _mapper.Map<IList<AddressEntity>, IList<OrderAddressViewModel>>(addresses);
        }

        public async Task<IList<OrderAddressViewModel>> GetAllCustomerBillingAddressAsync()
        {
            var addresses = await _orderRepository.GetAllCustomerAddressAsync(_userContext.UserId, IsBilling: true);

            return _mapper.Map<IList<AddressEntity>, IList<OrderAddressViewModel>>(addresses);
        }

        public async Task SaveBillingAddressAsync(Guid orderId, SaveOrderAddressViewModel address)
        {
            ArgumentGuard.NotNull(address, nameof(address));

            // TODO check for permissions;
            var addressEntity = _mapper.Map<SaveOrderAddressViewModel, AddressEntity>(address);
            addressEntity.IsBillingAddress = true;
            addressEntity.CustomerId = _userContext.UserId;

            await _orderRepository.SaveOrderAddressAsync(addressEntity);

            var order = await _orderRepository.GetAsync(orderId, isSummary: true);
            order.BillingAddressId = addressEntity.Id;

            await _orderRepository.UpdateFieldsAsync(order, nameof(order.BillingAddressId));
        }

        public async Task SaveShippingAddressAsync(Guid orderId, SaveOrderAddressViewModel model)
        {
            ArgumentGuard.NotDefault(orderId, nameof(orderId));
            ArgumentGuard.NotNull(model, nameof(model));

            var addressEntity = _mapper.Map<SaveOrderAddressViewModel, AddressEntity>(model);
            addressEntity.CustomerId = _userContext.UserId;

            // TODO Add permissions

            await _orderRepository.SaveOrderAddressAsync(addressEntity);

            var order = await _orderRepository.GetAsync(orderId, isSummary: true);
            order.ShippingAddressId = addressEntity.Id;

            await _orderRepository.UpdateFieldsAsync(order, nameof(order.ShippingAddressId));
        }

        public async Task SaveOrderAddressAsync(Guid orderId, int addressId)
        {
            ArgumentGuard.NotEmpty(orderId, nameof(orderId));
            ArgumentGuard.NotDefault(addressId, nameof(addressId));

            var order = await _orderRepository.GetAsync(orderId, isSummary: true);
            order.ShippingAddressId = addressId;

            await _orderRepository.UpdateFieldsAsync(order, nameof(order.ShippingAddressId));
        }

        public async Task SaveShippingMethod(Guid orderId, ShippingMethod shippingMethod)
        {
            ArgumentGuard.NotEmpty(orderId, nameof(orderId));
            
            if(shippingMethod == ShippingMethod.UnKnown)
            {
                throw new InvalidOperationException("You cant set shipping method to unknown");
            }

            var order = await _orderRepository.GetAsync(orderId, isSummary:true);
            order.ShippingMethod = shippingMethod;

            await _orderRepository.UpdateFieldsAsync(order, nameof(order.ShippingMethod));
        }
    }
}
