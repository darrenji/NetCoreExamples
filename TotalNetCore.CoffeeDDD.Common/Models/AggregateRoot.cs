using ReflectionMagic;
using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<Event> _changes = new List<Event>();
        public Guid Id { get; protected set; }
        public int Version { get; internal set; }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        /// <summary>
        /// 本质是让所有的事件执行一遍
        /// </summary>
        /// <param name="history"></param>
        public void LoadFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        /// <summary>
        /// 做两件事
        /// 一件是更改状态
        /// 一件是把状态放到集合中
        /// </summary>
        private void ApplyChange(Event @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            if (isNew) _changes.Add(@event);
        }
    }
}
