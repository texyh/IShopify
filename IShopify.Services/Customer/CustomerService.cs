
using AutoMapper;
using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using IShopify.Core.Exceptions;
using IShopify.Core.Helpers;
using IShopify.Core.MessageBus;
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
        private readonly ICustomerLookupService _customerLookUpService;
        private readonly IMessageBus _bus;

        private readonly IAccessKeyService _accessKeyService;

        private const int PasswordResetKeyExpiration = 1;
        


        public CustomerService(
            ICustomerRepository customerRepository,
            IUserContext userContext,
            IMapper mapper,
            ICustomerLookupService customerLookupService,
            IAccessKeyService accessKeyService,
            IMessageBus messageBus)
        {
            _customerRepository = customerRepository;
            _userContext = userContext;
            _mapper = mapper;
            _customerLookUpService = customerLookupService;
            _accessKeyService = accessKeyService;
            _bus = messageBus;
        }

        public async Task<Models.Customer> GetAsync()
        {
            var customerId = _userContext.UserId;
            var entity = await _customerRepository.GetAsync(customerId);

            return _mapper.Map<CustomerEntity, Models.Customer>(entity);
        }

        public async Task<string> GetResetPasswordAccesskey(int customerId)
        {
            ArgumentGuard.NotDefault(customerId, nameof(customerId));

            var expiryDate = DateTime.UtcNow.AddDays(PasswordResetKeyExpiration);
            var accessKey = await _accessKeyService.GenerateAccessKeyAsync(customerId, expiryDate, AccessScope.PasswordChange);

            return accessKey;
        }

        public async Task PasswordResetRequestAsync(string email)
        {
            ArgumentGuard.NotNull(email, nameof(email));

            var user = await _customerRepository.GetAsync(x => x.Email == email, true);
            
            if(user.IsNull())
            {
                throw new ObjectNotFoundException($"No customer with email ${email} exists");
            }

            await _bus.PublishAsync(new PasswordResetRequestCommand(user.Id, email));
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
    }
}
