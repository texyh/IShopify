
using System;
using IShopify.Core.Customer.Models;
using IShopify.Core.Data;
using IShopify.Core.Helpers;

namespace IShopify.Core.Security
{
    public class AccessKeyEntity : IEntity<Guid>
    {
        private string _scopes;

        public Guid Id { get; set; }

        public string Key { get; set; }

        public int UserId { get; set; }

        public DateTime ExpirationUtc { get; set; }

        public AccessScope[] Scopes 
        { 
            get
            {
                return ScopesJson.IsNull() ? null : ScopesJson.FromJson<AccessScope[]>();
            }
            
            set 
            {
                ScopesJson = value.ToJson();
            }
            
        }

        public string ScopesJson 
        {
            get 
            {
                if(_scopes.IsNull() && !Scopes.IsNullOrEmpty())
                {
                    return Scopes.ToJson();
                }

                return _scopes;
            }

            set { _scopes = value; }
        }

        public Guid[] ResourceIds { get; set; }

        public int MaxAllowedAttempts { get; set; }

        public int Attempts { get; set; }

        // when the key can be used
        public DateTime? StartTimeUtc { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime? DeletedDateUtc { get; set; }

        public virtual CustomerEntity Customer {get; set;}

        public DateTime? DeleteDateUtc {get; set;}
    }
}
