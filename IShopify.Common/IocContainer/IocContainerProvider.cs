using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Common.IocContainer
{
    public static class IocContainerProvider
    {
        public static IContainer Current { get; private set; }

        public static  void Register(IContainer container)
        {
            Current = container;
        }
    }
}
