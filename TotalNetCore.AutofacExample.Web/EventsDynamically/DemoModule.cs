using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;

namespace TotalNetCore.AutofacExample.Web.EventsDynamically
{
    public class DemoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>().InstancePerLifetimeScope();
            builder.RegisterType<DomainEventExecutor>().As<IDomainEventExecutor>().InstancePerLifetimeScope();

            RegisterEventHandlersFromDomainModel(builder);
        }

        private static void RegisterEventHandlersFromDomainModel(ContainerBuilder builder)
        {
            var domainAssembly = Assembly.GetAssembly(typeof(Entity));
            builder.RegisterAssemblyTypes(domainAssembly)
            .Where(t => t.GetInterfaces().Any(i => i.IsClosedTypeOf(typeof(IHandler<>))))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        }
    }
}
