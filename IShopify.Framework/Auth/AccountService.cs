using AutoMapper;
using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using IShopify.Core.Framework;
using IShopify.Core.Helpers;
using IShopify.Core.Security;
using IShopify.Framework.Auth.Models;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Framework.Auth
{
    public class AccountService : IAccountService
    {
        private readonly ICustomerRepository _customerReposiotry;
        private readonly ICryptoService _cryptoService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public AccountService(ICustomerRepository customerRepository, 
                              ICryptoService cryptoService,
                              IJwtHandler jwtHandler,
                              IMapper mapper)
        {
            _customerReposiotry = customerRepository;
            _cryptoService = cryptoService;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginCustomerAsync(CustomerLoginViewModel model)
        {
            ArgumentGuard.NotNull(model, nameof(model));
            ArgumentGuard.NotNullOrEmpty(model.Email, nameof(model.Email));
            ArgumentGuard.NotNullOrEmpty(model.Password, nameof(model.Password));

            var userEntity = await _customerReposiotry.GetAsync(x => x.Email == model.Email, true);

            if(userEntity.IsNull())
            {
                throw new InvalidCredentialException($"User with this {model.Email} does not exists");
            }

            var userPasswordHash = _cryptoService.Hash(model.Password, SecurityConstants.Salt, 3535);
            
            if(userEntity.Password != userPasswordHash)
            {
                throw new InvalidCredentialException($"Incorrect password");
            }

            var user = _mapper.Map<CustomerEntity, Customer>(userEntity);

            var token = _jwtHandler.CreateAccessToken(user);

            return new AuthenticationResponse { AccessToken = token };


        }

        public async Task<AuthenticationResponse> RegisterCustomerAsync(CustomerRegistrationViewModel model, bool isFaceBookRegistration = false)
        {
            ArgumentGuard.NotNull(model, nameof(model));
            ArgumentGuard.NotNullOrEmpty(model.Email, nameof(model.Email));
            ArgumentGuard.NotNullOrEmpty(model.Name, nameof(model.Name));

            var userExist = await _customerReposiotry.ExistsAsync(x => x.Email == model.Email);

            if(userExist)
            {
                throw new InvalidCredentialException($"User With ${model.Email} Already Exists");
            }

            var customer = new CustomerEntity
            {
                FirstName = model.Name,
                LastName = model.LastName,
                Email = model.Email,
            };

            if(!isFaceBookRegistration && !model.Password.IsNullOrEmpty())
            {
                //var salt = _cryptoService.GenerateSalt(32);
                var hash = _cryptoService.Hash(model.Password, SecurityConstants.Salt, 3535); // TODO get salt from env
                customer.Password = hash;
                await _customerReposiotry.AddAsync(customer);
            }

            if(isFaceBookRegistration)
            {
                await _customerReposiotry.AddAsync(customer);
            }

            var user = _mapper.Map<CustomerEntity, Customer>(customer);

            var token = _jwtHandler.CreateAccessToken(user);

            return new AuthenticationResponse { AccessToken = token };
        }
    }

    public interface IAccountService
    {
        Task<AuthenticationResponse> RegisterCustomerAsync(CustomerRegistrationViewModel model, bool isFaceBookRegistration =  false);

        Task<AuthenticationResponse> LoginCustomerAsync(CustomerLoginViewModel model);
    }
}
