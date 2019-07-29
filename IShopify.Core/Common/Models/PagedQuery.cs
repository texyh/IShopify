using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Common.Models
{
    public class PagedQuery
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public void NormalizePageNumber()
        {
            if (PageNumber == 0)
            {
                PageNumber = 1;
            }
        }
    }
}
