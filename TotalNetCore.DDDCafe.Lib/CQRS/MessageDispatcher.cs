using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TotalNetCore.DDDCafe.Lib.CQRS
{
    public class MessageDispatcher
    {
        private Dictionary<Type, Action<object>> commandHandlers = new Dictionary<Type, Action<object>>();
        private Dictionary<Type, List<Action<object>>> eventSubscribers = new Dictionary<Type, List<Action<object>>>();
        private IEventStore eventStore;

        public MessageDispatcher(IEventStore es)
        {
            eventStore = es;
        }

        public void SendCommand<TCommand>(TCommand c)
        {
            if(commandHandlers.ContainsKey(typeof(TCommand)))
            {
                commandHandlers[typeof(TCommand)](c);
            }
            else
            {
                throw new Exception($"No command handler registered for {typeof(TCommand).Name}");
            }
        }

        private object CreateInstanceOf(Type t)
        {
            return Activator.CreateInstance(t);
        }

        /// <summary>
        /// 发布事件：找到对象的所有委托并执行
        /// </summary>
        /// <param name="e"></param>
        private void PublishEvent(object e)
        {
            var eventType = e.GetType();
            if(eventSubscribers.ContainsKey(eventType))
            {
                foreach(var sub in eventSubscribers[eventType])
                {
                    sub(e);
                }
            }
        }

        //Command的处理
        public void AddHandlerFor<TCommand,TAggregate>() where TAggregate : Aggregate, new()
        {
            if(commandHandlers.ContainsKey(typeof(TCommand)))
            {
                throw new Exception($"Command handler already registered for {typeof(TCommand).Name}");
            }

            commandHandlers.Add(typeof(TCommand), c => {
                var agg = new TAggregate();

                agg.Id = ((dynamic)c).Id;
                agg.ApplyEvents(eventStore.LoadEventsFor<TAggregate>(agg.Id));//领域执行所有事件

                var resultEvents = new List<Event>();//所有执行的事件
                foreach(var e in (agg as IHandleCommand<TCommand>).Handle((TCommand)c))
                {
                    resultEvents.Add(e);
                }

                if(resultEvents.Count>0)
                {
                    eventStore.SaveEventsFor<TAggregate>(agg.Id, agg.EventsLoaded, resultEvents);//保存事件
                }

                foreach(var e in resultEvents)
                {
                    PublishEvent(e);//发布事件
                }
            });
        }

        //事件订阅
        public void AddSubscriberFor<TEvent>(ISubscribeTo<TEvent> subscriber) where TEvent: Event
        {
            if(!eventSubscribers.ContainsKey(typeof(TEvent)))
            {
                eventSubscribers.Add(typeof(TEvent), new List<Action<object>>());
            }

            eventSubscribers[typeof(TEvent)].Add(e => subscriber.Hanle((TEvent)e));
        }

        public void ScanAssembly(Assembly ass)
        {
            var handlers =
                from t in ass.GetTypes()
                from i in t.GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(IHandleCommand<>)
                let args = i.GetGenericArguments()
                select new
                {
                    CommandType = args[0],
                    AggregateType = t
                };

            foreach(var h in handlers)
            {
                this.GetType().GetMethod("AddHandlerFor")
                    .MakeGenericMethod(h.CommandType, h.AggregateType)
                    .Invoke(this, new object[] { });
            }

            // Scan for and register subscribers.
            var subscriber =
                from t in ass.GetTypes()
                from i in t.GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(ISubscribeTo<>)
                select new
                {
                    Type = t,
                    EventType = i.GetGenericArguments()[0]
                };

            foreach (var s in subscriber)
                this.GetType().GetMethod("AddSubscriberFor")
                    .MakeGenericMethod(s.EventType)
                    .Invoke(this, new object[] { CreateInstanceOf(s.Type) });
        }

        public void ScanInstance(object instance)
        {
            // Scan for and register handlers.
            var handlers =
                from i in instance.GetType().GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(IHandleCommand<>)
                let args = i.GetGenericArguments()
                select new
                {
                    CommandType = args[0],
                    AggregateType = instance.GetType()
                };
            foreach (var h in handlers)
                this.GetType().GetMethod("AddHandlerFor")
                    .MakeGenericMethod(h.CommandType, h.AggregateType)
                    .Invoke(this, new object[] { });

            // Scan for and register subscribers.
            var subscriber =
                from i in instance.GetType().GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(ISubscribeTo<>)
                select i.GetGenericArguments()[0];
            foreach (var s in subscriber)
                this.GetType().GetMethod("AddSubscriberFor")
                    .MakeGenericMethod(s)
                    .Invoke(this, new object[] { instance });
        }
    }
}
