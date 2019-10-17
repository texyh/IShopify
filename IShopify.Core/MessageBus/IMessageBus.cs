using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IShopify.Core.MessageBus
{
    public interface IMessageBus
    {
        Task PublishAsync<TMessage>(TMessage message, CancellationToken cancelationToken = default(CancellationToken))
            where TMessage : class, IMessage;
    }
}
