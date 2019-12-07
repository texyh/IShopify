using AutoMapper;
using IShopify.Core.Categories;
using IShopify.Core.Categories.Models;
using IShopify.Core.Common;
using IShopify.Core.Data;
using IShopify.Core.Exceptions;
using IShopify.Core.Helpers;
using IShopify.DomainServices.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.DomainServices.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IPermissionFactory _permissionFactory;

        private readonly IValidatorFactory _validationFactory;

        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IPermissionFactory permissionFactory,
            IValidatorFactory validatorFactory,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _permissionFactory = permissionFactory;
            _validationFactory = validatorFactory;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(SaveCategoryModel model)
        {
            ArgumentGuard.NotNull(model, nameof(model));

            var cat = _mapper.Map<SaveCategoryModel, Category>(model);

            cat.SetPermission(_permissionFactory.CreateCategoryPermission());

            if(!cat.Permissions.CanCreate)
            {
                throw new InvalidPermissionException("you do not have permission to create this category");
            }

            await _validationFactory.ValidateAsync(cat);

            var entity = _mapper.Map<Category, CategoryEntity>(cat);

            return await _categoryRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _categoryRepository.GetAsync(id);

            var cat = _mapper.Map<CategoryEntity, Category>(entity);

            cat.SetPermission(_permissionFactory.CreateCategoryPermission());

            if(!cat.Permissions.CanDelete)
            {
                throw new InvalidPermissionException("you do not have the permission to delete this category");
            }

            await _categoryRepository.DeleteAsync(entity);
        }

        public async Task<IList<Category>> GetAllAsync()
        {
            var entities = await _categoryRepository.FindAllAsync();
            
            return _mapper.Map<IList<CategoryEntity>, IList<Category>>(entities);
        }

        public async Task<Category> GetAsync(int id)
        {
            var entity = await _categoryRepository.GetAsync(id);

            var category = _mapper.Map<CategoryEntity, Category>(entity);

            category.SetPermission(_permissionFactory.CreateCategoryPermission());

            if(!category.Permissions.CanView)
            {
                throw new InvalidPermissionException("you do not have access to view this category");
            }

            return category;
        }

        public Task UpdateAsync(int id, SaveCategoryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
