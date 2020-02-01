using Autofac;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.AutofacExample.Web.EventsDynamically
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly ILifetimeScope _lifetimeScope;//实现IComponentContext

        public EventDispatcher(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }
        public void Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            foreach(dynamic handler in GetHandlers(eventToDispatch))
            {
                handler.Handle((dynamic)eventToDispatch);
            }
        }

        private IEnumerable GetHandlers<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            //找到当前IDomainEvent的IHandler<>的实现类
           return (IEnumerable)_lifetimeScope.Resolve(typeof(IEnumerable<>).MakeGenericType(typeof(IHandler<>).MakeGenericType(eventToDispatch.GetType())));
        }
    }
}
