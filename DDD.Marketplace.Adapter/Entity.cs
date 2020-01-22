using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.Marketplace.Adapter
{
    /// <summary>
    /// 领域的基类
    /// </summary>
    public abstract class Entity<TId> : IInternalEventHandler where TId : Value<TId>
    {
        private readonly Action<object> _applier;

        public TId Id { get; protected set; }

        protected Entity(Action<object> applier)
        {
            _applier = applier;
        }

        public void Handle(object @event)
        {
            When(@event);
        }

        protected abstract void When(object @event);

        protected void Apply(object @event)
        {
            When(@event);
            _applier(@event);
        }
    }
}
