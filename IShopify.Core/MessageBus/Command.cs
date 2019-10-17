using IShopify.Core.MessageBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.MessageBus
{
    public abstract class Command : IMessage
    {
        protected Command(int userId)
        {
            CommandId = Guid.NewGuid();
            UserId = userId;
            CreatedDateUtc = DateTime.UtcNow;
        }

        public Guid CommandId { get; }

        public int UserId { get; }

        public DateTime CreatedDateUtc { get; }
    }
}
