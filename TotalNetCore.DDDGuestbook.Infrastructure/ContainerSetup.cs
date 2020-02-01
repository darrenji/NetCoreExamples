using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using TotalNetCore.DDDGuestbook.Core.SharedKernel;
using TotalNetCore.DDDGuestbook.Infrastructure.Data;

namespace TotalNetCore.DDDGuestbook.Infrastructure
{
    public static class ContainerSetup
    {
        public static IServiceProvider InitializeWeb(Assembly webAssembly, IServiceCollection services) =>
            new AutofacServiceProvider(BaseAutofacInitialization(setupAction =>//AutofacServiceProvider是IServiceProvider的实现，接收IContainer
            {
                setupAction.Populate(services);//让默认的服务、IServiceProvider,IServiceScopeFactory在Autofac的ContainerBuilder中可用
                setupAction.RegisterAssemblyTypes(webAssembly).AsImplementedInterfaces();//注册程序集中的所有接口和实现
            }));

        public static Autofac.IContainer BaseAutofacInitialization(Action<ContainerBuilder> setupAction = null)
        {
            var builder = new ContainerBuilder();

            var coreAssembly = Assembly.GetAssembly(typeof(BaseEntity));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository));
            builder.RegisterAssemblyTypes(coreAssembly, infrastructureAssembly).AsImplementedInterfaces();//先注册

            setupAction?.Invoke(builder);//触发方法
            return builder.Build();
        }
    }
}
