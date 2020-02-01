using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.AutofacExample.Web.EventsDynamically
{
    /// <summary>
    /// 处理所有领域的所有事件
    /// </summary>
    public class DomainEventExecutor : IDomainEventExecutor
    {
        private readonly IEventDispatcher _domainEventDispatcher;

        public DomainEventExecutor(IEventDispatcher domainEventDispatcher)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }
        public void Execute(IEnumerable<IEntity> domainEventEntities)
        {
            foreach(var entity in domainEventEntities)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();

                foreach(var @event in events)
                {
                    _domainEventDispatcher.Dispatch(@event);
                }
            }
        }
    }
}
