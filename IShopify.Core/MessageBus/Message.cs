using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.MessageBus
{
    public class Message : IMessage
    {
        public Message(int userId)
        {
            UserId = userId;
            CreatedDateUtc = DateTime.UtcNow;
        }

        public int UserId { get; }

        public DateTime CreatedDateUtc { get; }
    }
}
