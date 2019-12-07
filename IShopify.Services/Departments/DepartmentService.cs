using AutoMapper;
using IShopify.Core.Common;
using IShopify.Core.Data;
using IShopify.Core.Departments;
using IShopify.Core.Departments.Models;
using IShopify.Core.Exceptions;
using IShopify.Core.Helpers;
using IShopify.DomainServices.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.DomainServices.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        private readonly IMapper _mapper;

        private readonly IPermissionFactory _permissionFactory;

        private readonly IValidatorFactory _validationFactory;

        public DepartmentService(
            IDepartmentRepository departmentRepository,
            IMapper mapper,
            IPermissionFactory permissionFactory,
            IValidatorFactory validatorFactory)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _permissionFactory = permissionFactory;
            _validationFactory = validatorFactory;
        }

        public async Task<Department> GetAsync(int id)
        {
            var entity = await _departmentRepository.GetAsync(id);

            var deparment = _mapper.Map<DepartmentEntity, Department>(entity);

            deparment.SetPermissions(_permissionFactory.CreateDepartmentPermissions());

            if(!deparment.Permissions.CanView)
            {
                throw new InvalidPermissionException("you do not have permission to view this deparment");
            }

            return deparment;
        }

        public async Task<IList<Department>> GetAllAsync()
        {
            var departments = await _departmentRepository.FindAllAsync();

            return _mapper.Map<IList<DepartmentEntity>, IList<Department>>(departments);
        }

        public Task<int> CreateAsync(SaveDepartmentModel model)
        {
            ArgumentGuard.NotNull(model, nameof(model));

            var department = _mapper.Map<SaveDepartmentModel, Department>(model);

            department.SetPermissions(_permissionFactory.CreateDepartmentPermissions());

            if(!department.Permissions.CanCreate)
            {
                throw new InvalidPermissionException("you do not have the permission to create this department");
            }

            _validationFactory.ValidateAsync(department);

            var entity = _mapper.Map<Department, DepartmentEntity>(department);

            return _departmentRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, SaveDepartmentModel model)
        {
            var entity = await _departmentRepository.GetAsync(id);

            var department = _mapper.Map<DepartmentEntity, Department>(entity);

            department.SetPermissions(_permissionFactory.CreateDepartmentPermissions());

            if(!department.Permissions.CanEdit)
            {
                throw new InvalidPermissionException("you do not have permission to edit this permission");
            }

            var updatedFields = ApplyUpdates(department, model);

            if(updatedFields.IsNull())
            {
                return;
            }

            await _validationFactory.ValidateAsync(department);

            var depEntity = _mapper.Map<Department, DepartmentEntity>(department);

            await _departmentRepository.UpdateFieldsAsync(depEntity, updatedFields.ToArray());
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        private IList<string> ApplyUpdates(Department oldDepartment, SaveDepartmentModel newDepartment)
        {
            var updatedFields = new List<string>();

            if(!newDepartment.Name.IsNullOrEmpty() && newDepartment.Name != oldDepartment.Name)
            {
                updatedFields.Add(nameof(newDepartment.Name));
                oldDepartment.Name = newDepartment.Name;
            }

            if (!newDepartment.Description.IsNullOrEmpty() && newDepartment.Description != oldDepartment.Description)
            {
                updatedFields.Add(nameof(newDepartment.Description));
                oldDepartment.Description = newDepartment.Description;
            }

            return updatedFields;
        }
    }
}
