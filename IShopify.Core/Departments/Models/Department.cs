using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Departments
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IDepartmentPermissions Permissions { get; private set; }

        public void SetPermissions(IDepartmentPermissions permissions)
        {
            Permissions = permissions;
        }
    }
}
