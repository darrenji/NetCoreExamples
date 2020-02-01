using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TotalNetCore.DDDEventPattern
{
    public static class Container
    {
        public static Autofac.IContainer Resolve()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<EventModule>();

            return builder.Build();
        }
    }
}
