using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Autofac;

namespace TotalNetCore.DDDAPISample.Infrastructure.Quartz
{
    public class JobFactory : IJobFactory
    {
        private readonly Autofac.IContainer _container;

        public JobFactory(Autofac.IContainer container)
        {
            this._container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var job = _container.Resolve(bundle.JobDetail.JobType);

            return job as IJob;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
