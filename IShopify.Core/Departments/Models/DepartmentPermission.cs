using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Departments.Models
{
    public class DepartmentPermission : IDepartmentPermissions
    {
        private readonly  Lazy<bool> _canCreate;

        private readonly Lazy<bool> _canEdit;

        private readonly Lazy<bool> _canView;

        public DepartmentPermission()
        {
            _canCreate = new Lazy<bool>(IsCanCreate);
            _canEdit = new Lazy<bool>(IsCanEdit);
            _canView = new Lazy<bool>(IsCanView);
        }

        public bool CanEdit => _canEdit.Value;

        public bool CanCreate => _canCreate.Value;

        public bool CanView => _canView.Value;

        private bool IsCanCreate()
        {
            return true;
        }

        private bool IsCanEdit()
        {
            return true;
        }

        private bool IsCanView()
        {
            return true;
        }
    }
}
