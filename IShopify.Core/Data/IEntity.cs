using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Data
{
    public interface IEntity<T> where T : struct
    {
        T Id { get; }
    }
}
