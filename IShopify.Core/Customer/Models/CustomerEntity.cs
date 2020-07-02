using IShopify.Core.Data;
using IShopify.Core.Helpers;
using IShopify.Core.Orders.Models.Entities;
using IShopify.Core.Orders.Models.Entity;
using IShopify.Core.Security;
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
            AccessKeys = new List<AccessKeyEntity>();
        }

        private string _authProfile {get; set;}

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName {get;set;}

        public string Email { get; set; }

        public DateTime DateofBirth { get; set; }

        public CustomerAuthProfile AuthProfile
        {
            get => AuthProfileJson.IsNullOrEmpty() 
                    ? null 
                    : AuthProfileJson.FromJson<CustomerAuthProfile>();

            set { AuthProfileJson  = value.ToJson(); }
        }

        public string AuthProfileJson 
        {
            get
            {
                if(_authProfile.IsNull() && !AuthProfile.IsNull()) 
                {
                    return AuthProfile.ToJson();
                }

                return _authProfile;
            }

            set {_authProfile = value;}
        }

        public virtual ICollection<OrderEntity> Orders { get; set; }

        public virtual ICollection<ReviewEntity> Reviews { get; set; }

        public virtual ICollection<AddressEntity> Addresses { get; set; }

        public virtual ICollection<AccessKeyEntity> AccessKeys {get; set;}

        public DateTime? DeleteDateUtc {get; set;}
    }
}
