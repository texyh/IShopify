using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Common.Models
{
    public class PagedResult<T>
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int Count => Items.Count;

        public IList<T> Items { get; set; }
    }
}
