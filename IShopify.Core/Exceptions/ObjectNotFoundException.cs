using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Exceptions
{
    public class ObjectNotFoundException : AppException
    {
        public ObjectNotFoundException(string message, string friendlyMessage = null)
            : base(message, friendlyMessage)
        {
        }
    }
}
