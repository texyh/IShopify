using AutoMapper;
using IShopify.Core.Customer;
using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using IShopify.Core.Framework;
using IShopify.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = IShopify.Core.Customer.Models;

namespace IShopify.DomainServices.Customer
{
    public class CustomerLookupService : ICustomerCacheService
    {
        private const int CacheDurationInMinutes = 5;

        private readonly IRedisCacheService _redisCacheService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerLookupService(
            IRedisCacheService redisCache,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _redisCacheService = redisCache;
            _mapper = mapper;
        }

        public void Add(Models.Customer customer)
        {
            _redisCacheService.Add(GetCacheKey(customer.Id), customer, TimeSpan.FromMinutes(CacheDurationInMinutes));
        }

        public async Task<Models.Customer> GetCustomerAsync(int id)
        {
            var key = GetCacheKey(id);
            var customer = _redisCacheService.Get<Models.Customer>(key);

            if (customer.IsNull())
            {
                var customerEntity = await _customerRepository.GetAsync(id);

                customer = _mapper.Map<Models.Customer>(customerEntity);

                Add(customer);
            }

            return customer;
        }

        public async Task<IList<Models.Customer>> GetCustomersAsync(IEnumerable<int> Ids)
        {
            var keys = Ids.Select(id => GetCacheKey(id));
            var cachedUsers = _redisCacheService.GetAll<Models.Customer>(keys);

            if (cachedUsers.Count == Ids.Count())
            {
                return cachedUsers;
            }

            var userIdsNotCached = Ids.Where(id => !cachedUsers.Any(user => user.Id == id));
            var customerEntities = await _customerRepository.FindAllInIdsAsync(userIdsNotCached);
            var customers = _mapper.Map<IList<Models.Customer>>(customerEntities);

            foreach (var user in customers)
            {
                Add(user);
                cachedUsers.Add(user);
            }

            return cachedUsers;
        }

        public void Remove(int id)
        {
            _redisCacheService.Remove(GetCacheKey(id));
        }

        public void Update(Models.Customer customer)
        {
            var key = GetCacheKey(customer.Id);

            _redisCacheService.Remove(key);
            _redisCacheService.Add(key, customer, TimeSpan.FromMinutes(CacheDurationInMinutes));
        }

        private string GetCacheKey(int id)
        {
            return $"Customer::{id}";
        }
    }
}
