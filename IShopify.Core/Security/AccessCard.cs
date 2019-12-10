using System;
using System.Collections.Generic;
using System.Linq;

namespace IShopify.Core.Security
{
    public class AccessCard
    {
        public AccessCard(string key, int userId, AccessKeyStatus status, 
            IEnumerable<AccessScope> scopes, IEnumerable<Guid> resourceIds)
        {
            Key = key;
            UserId = userId;
            Status = status;
            Scopes = scopes?.ToArray() ?? new AccessScope[0];
            ResourceIds = resourceIds?.ToArray() ?? new Guid[0];
        }

        public string Key { get; }

        public int UserId { get;  }

        public AccessScope[] Scopes { get; }

        public Guid[] ResourceIds { get; }

        public AccessKeyStatus Status { get; }

        public bool IsValid => Status == AccessKeyStatus.Valid;
    }
}
