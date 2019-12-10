using System;

namespace IShopify.Core.Security
{
    public class CreateAccessKeyModel
    {
        public int UserId { get; set; }

        public DateTime ExpirationUtc { get; set; }

        public AccessScope[] Scopes { get; set; }

        public Guid[] ResourceIds { get; set; }

        public int MaxAllowedAttempts { get; set; }

        // when the key can be used
        public DateTime? StartTimeUtc { get; set; }
    }
}
