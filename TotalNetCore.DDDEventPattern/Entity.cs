using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDEventPattern
{
    public class Entity : IEntity
    {
        //使用字典集可以避免事件名称重复
        private readonly IDictionary<Type, IEvent> _events = new Dictionary<Type, IEvent>();
        public IEnumerable<IEvent> Events => _events.Values;

        protected void AddEvent(IEvent @event)
        {
            _events[@event.GetType()] = @event;
        }

        protected void ClearEvents()
        {
            _events.Clear();
        }
    }
}
