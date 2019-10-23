using IShopify.Core.Attributes;
using IShopify.Core.Attributes.Models;
using IShopify.Core.Common;
using IShopify.Core.Common.Models;
using IShopify.Core.Data;
using IShopify.Core.Exceptions;
using IShopify.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.DomainServices.Attribure
{
    public class AttributeService : IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository;

        private readonly IPermissionFactory _permissionFactory;

        public AttributeService(
            IAttributeRepository attributeRepository,
            IPermissionFactory permissionFactory)
        {
            _attributeRepository = attributeRepository;
            _permissionFactory = permissionFactory;
        }

        public Task<int> AddAsync(string attribute)
        {
            ArgumentGuard.NotNullOrEmpty(attribute, nameof(attribute));

            var permissions = _permissionFactory.CreateAttributePermissions();

            if(!permissions.CanCreate)
            {
                throw new InvalidPermissionException("you do no have permission to create this attribue");
            }

            return _attributeRepository.AddAsync(new AttributeEntity { Name = attribute });
        }

        public async Task<int> AddValueAsync(int attributeId, string value)
        {
            ArgumentGuard.NotNullOrEmpty(value, nameof(value));
            ArgumentGuard.NotDefault(attributeId, nameof(attributeId));

            // TODO add permission to check if you can add object;

            return await _attributeRepository.AddValueAsync(new AttributeValueEntity { AttributeId = attributeId, Value = value });
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteValueAsync(int valueId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<NamedId<int>>> GetAllAsync()
        {
            var entity =  await _attributeRepository.FindAllAsync();

            // TODO add permissions

            return entity.Select(x => new NamedId<int> { Id = x.Id, Name = x.Name }).ToList();
        }

        public async Task<NamedId<int>> GetAsync(int id)
        {
            var entity = await _attributeRepository.GetAsync(id);
            // TODO add permissions

            return new NamedId<int> { Id = entity.Id, Name = entity.Name };
        }

        public Task<IList<NamedId<int>>> GetValuesAsync(int attributeId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, string attribute)
        {
            throw new NotImplementedException();
        }

        public Task UpdateValueAsync(int id, int attributeId, string value)
        {
            throw new NotImplementedException();
        }
    }
}
