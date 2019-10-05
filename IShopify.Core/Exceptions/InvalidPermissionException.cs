using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Exceptions
{
    [Serializable]
    public class InvalidPermissionException : AppException
    {
        public InvalidPermissionException(string message) : base(message, message)
        {

        }
    }
}
