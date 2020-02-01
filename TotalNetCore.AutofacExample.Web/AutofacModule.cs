using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using System.Threading.Tasks;
using TotalNetCore.AutofacExample.Web.Services;
using Microsoft.Extensions.Logging;

namespace TotalNetCore.AutofacExample.Web
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //ILogger<T>需要在ConfigureService中被注册到ServiceCollection
            //当使用Populate方法的时候，ServiceCollection中的服务就来到了Autofac的容器中
            builder.Register(c => new ValuesService(c.Resolve<ILogger<ValuesService>>()))
                .As<IValuesService>()
                .InstancePerLifetimeScope();
        }
    }
}
