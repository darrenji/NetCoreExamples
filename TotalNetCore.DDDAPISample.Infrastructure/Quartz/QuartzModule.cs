
using Autofac;
using Quartz;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TotalNetCore.DDDAPISample.Infrastructure.Quartz
{
    public class QuartzModule : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            try
            {
                builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
               .Where(x => typeof(IJob).IsAssignableFrom(x)).InstancePerDependency();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
