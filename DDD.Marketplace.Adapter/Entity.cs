using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDD.Marketplace.Adapter
{
    public abstract class Entity
    {
        //用来存放事件
        private readonly List<object> _events;

        protected Entity()
        {
            _events = new List<object>();
        }

        //把事件放集合中
        protected void Raise(object @event)
        {
            _events.Add(@event);
        }

        public IEnumerable<object> GetChanges()
        {
            return _events.AsEnumerable();
        }

        public void ClearChanges()
        {
             _events.Clear();
        }
    }
}
