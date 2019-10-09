
using AutoMapper;
using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using IShopify.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models = IShopify.Core.Customer.Models;

namespace IShopify.DomainServices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository, 
            IUserContext userContext,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<Models.Customer> GetAsync()
        {
            var customerId = _userContext.UserId;
            var entity = await _customerRepository.GetAsync(customerId);

            return _mapper.Map<CustomerEntity, Models.Customer>(entity);
        }

        public async Task<Models.Customer> UpdateCustomerAddressAsync(SaveCustomerAddressViewModel model)
        {
            var customerId = _userContext.UserId;
            var entity = await _customerRepository.GetAsync(customerId);
            var updatedEntity = _mapper.Map(model, entity);

            await _customerRepository.UpdateAsync(updatedEntity);

            return _mapper.Map<CustomerEntity, Models.Customer>(updatedEntity);
        }

        public async Task<Models.Customer> UpdateCustomerAsync(SaveCustomerViewModel model)
        {
            var customerId = _userContext.UserId;
            var entity = await _customerRepository.GetAsync(customerId);
            var updatedEntity = _mapper.Map(model, entity);

            await _customerRepository.UpdateAsync(updatedEntity);

            return _mapper.Map<CustomerEntity, Models.Customer>(updatedEntity);
        }

        public async Task UpdateCustomerCreditCardAsync(string creditCard)
        {
            var customerId = _userContext.UserId;
            var entity = new CustomerEntity { Id = customerId, CreditCard = creditCard };

            await _customerRepository.UpdateFieldsAsync(entity, nameof(entity.CreditCard));
        }
    }
}
