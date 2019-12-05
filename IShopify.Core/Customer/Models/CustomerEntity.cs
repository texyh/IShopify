using IShopify.Core.Data;
using IShopify.Core.Orders.Models.Entities;
using IShopify.Core.Orders.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Customer.Models
{
    public class CustomerEntity : IEntity<int>
    {
        public CustomerEntity()
        {
            Reviews = new List<ReviewEntity>();
            Orders = new List<OrderEntity>();
            Addresses = new List<AddressEntity>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName {get;set;}

        public string Email { get; set; }

        public DateTime DateofBirth { get; set; }

        public string Password { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }

        public virtual ICollection<ReviewEntity> Reviews { get; set; }

        public virtual ICollection<AddressEntity> Addresses { get; set; }
    }
}
