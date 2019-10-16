using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.MessageBus
{
    public interface IMessage
    {
        int UserId { get; }

        DateTime CreatedDateUtc { get; }
    }
}
