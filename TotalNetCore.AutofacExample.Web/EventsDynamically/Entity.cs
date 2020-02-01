using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.AutofacExample.Web.EventsDynamically
{
    public class Entity : IEntity
    {
        //使用字典集可以避免事件名称重复
        private readonly IDictionary<Type, IDomainEvent> _events = new Dictionary<Type, IDomainEvent>();
        public List<IDomainEvent> Events => _events.Values.ToList();

        protected void AddEvent(IDomainEvent @event)
        {
            _events[@event.GetType()] = @event;
        }

        protected void ClearEvents()
        {
            _events.Clear();
        }
    }
}
