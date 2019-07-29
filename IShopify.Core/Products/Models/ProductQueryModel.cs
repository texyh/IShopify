using IShopify.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Products.Models
{
    public class ProductQueryModel : PagedQuery
    {
        public string SearchText { get; set; }
    }
}
