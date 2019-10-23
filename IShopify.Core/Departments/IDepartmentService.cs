using IShopify.Core.Departments.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IShopify.Core.Departments
{
    public interface IDepartmentService
    {
        Task<Department> GetAsync(int id);

        Task<IList<Department>> GetAllAsync();

        Task<int> CreateAsync(SaveDepartmentModel model);

        Task UpdateAsync(int id, SaveDepartmentModel model);

        Task DeleteAsync(int id);
    }
}
