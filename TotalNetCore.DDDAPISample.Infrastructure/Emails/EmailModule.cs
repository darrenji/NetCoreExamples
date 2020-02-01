using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TotalNetCore.DDDAPISample.Application.Configuration.Emails;

namespace TotalNetCore.DDDAPISample.Infrastructure.Emails
{
    public class EmailModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailSender>()
                .As<IEmailSender>()
                .InstancePerLifetimeScope();
        }
    }
}
