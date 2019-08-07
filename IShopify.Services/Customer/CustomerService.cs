
using AutoMapper;
using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.DomainServices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetAsync()
        {
            var customerId = 1; // TODO get id from userContext;
            var entity = await _customerRepository.GetAsync(customerId);

            return Mapper.Map<CustomerEntity, Customer>(entity);
        }

        public async Task<Customer> UpdateCustomerAddressAsync(SaveCustomerAddressViewModel model)
        {
            var customerId = 1; // TODO get id for userContext;
            var entity = await _customerRepository.GetAsync(customerId);
            var updatedEntity = Mapper.Map(model, entity);

            await _customerRepository.UpdateAsync(updatedEntity);

            return Mapper.Map<CustomerEntity, Customer>(updatedEntity);
        }

        public async Task<Customer> UpdateCustomerAsync(SaveCustomerViewModel model)
        {
            var customerId = 1; // TODO get id for userContext;
            var entity = await _customerRepository.GetAsync(customerId);
            var updatedEntity = Mapper.Map(model, entity);

            await _customerRepository.UpdateAsync(updatedEntity);

            return Mapper.Map<CustomerEntity, Customer>(updatedEntity);
        }

        public async Task UpdateCustomerCreditCardAsync(string creditCard)
        {
            var customerId = 1; // TODO get id for userContext;
            var entity = new CustomerEntity { Id = customerId, CreditCard = creditCard };

            await _customerRepository.UpdateSingleField(entity, x => x.CreditCard);
        }
    }
}
