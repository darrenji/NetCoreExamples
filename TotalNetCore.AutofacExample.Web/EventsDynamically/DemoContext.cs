using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.AutofacExample.Web.EventsDynamically
{
    public class DemoContext : DbContext
    {
        private readonly IDomainEventExecutor _domainEventExecutor;
        public DemoContext(DbContextOptions<DemoContext> options, IDomainEventExecutor domainEventExecutor) : base(options)
        {
            _domainEventExecutor = domainEventExecutor;
        }

        public override int SaveChanges()
        {
            var n = base.SaveChanges();

            //再持久化数据库之后把所有领域的所有事件执行
            _domainEventExecutor.Execute(GetDomainEventEntities());

            return n;
        }


        private IEnumerable<IEntity> GetDomainEventEntities()
        {
            return ChangeTracker.Entries<IEntity>()
                .Select(t => t.Entity)
                .Where(t => t.Events.Any())
                .ToArray();
        }
    }
}
