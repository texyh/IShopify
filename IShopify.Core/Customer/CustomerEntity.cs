using IShopify.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Customer
{
    public class CustomerEntity : IEntity
    {
        public CustomerEntity()
        {
            Reviews = new List<ReviewEntity>();
        }

        public int Id { get; set; }

        public string Name { get; set; }


        public virtual ICollection<ReviewEntity> Reviews { get; set; }
    }
}
