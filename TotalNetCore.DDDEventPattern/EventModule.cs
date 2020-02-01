using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TotalNetCore.DDDEventPattern
{
    public class EventModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventDispatcher>()
                .As<IEventDispatcher>()
                .InstancePerLifetimeScope();

            var assembly = Assembly.Load(new AssemblyName("MyAssemblyNameSpace"));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEventHandler<>));
        }
    }
}
