using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class ProductPermissions : IProductPermissions
    {
        private readonly Lazy<bool> _canCreate;

        private readonly Lazy<bool> _canEdit;

        private readonly Lazy<bool> _canView;

        public ProductPermissions()
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
            return true; // TODO change implementation
        }

        private bool IsCanEdit()
        {
            return true; // TODO change implementation
        }

        private bool IsCanView()
        {
            return true; // TODO change implementation
        }
    }
}
