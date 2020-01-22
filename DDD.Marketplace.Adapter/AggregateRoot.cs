using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.Marketplace.Adapter
{
    public abstract class AggregateRoot<TId> : IInternalEventHandler where TId : Value<TId>
    {
        public void Handle(object @event)
        {
            When(@event);
        }

        public TId Id { get; protected set; }

        private readonly List<object> _changes;

        protected abstract void When(object @event);//给子类实现，用来改变领域状态

        protected AggregateRoot()
        {
            _changes = new List<object>();
        }

        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();
        public void ClearChanges() => _changes.Clear();
        protected abstract void EnsureValiedState();

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValiedState();
            _changes.Add(@event);
        }

        //这里神来之笔，让聚合类中的子类执行事件，从而保证聚合类和子类状态的一致性
        protected void ApplyToEntity(IInternalEventHandler entity, object @event) => entity?.Handle(@event);
    }
}
