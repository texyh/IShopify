using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Common.Models
{
    public class NamedId<T>
    {
        public T Id { get; set; }

        public string Name { get; set; }
    }
}
