﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TotalNetCore.SimpleCQRS.Lib
{
    public class FakeBus : ICommandSender, IEventPublisher
    {
        private readonly Dictionary<Type, List<Action<Message>>> _routes = new Dictionary<Type, List<Action<Message>>>();

        public void RegisterHandler<T>(Action<T> handler)  where T : Message
        {
            List<Action<Message>> handlers;

            if(!_routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<Message>>();
                _routes.Add(typeof(T), handlers);
            }

            handlers.Add(x=> handler((T)x));
        }

        public void Publish<T>(T @event) where T : Event
        {
            List<Action<Message>> handlers;

            if (!_routes.TryGetValue(@event.GetType(), out handlers)) return;

            foreach(var handler in handlers)
            {
                var handler1 = handler;
                ThreadPool.QueueUserWorkItem(x => handler1(@event));
            }
        }

        public void Send<T>(T command) where T : Command
        {
            List<Action<Message>> handlers;

            if(_routes.TryGetValue(typeof(T), out handlers))
            {

                if (handlers.Count != 1) throw new InvalidOperationException("cannot send to more than one handler");

                handlers[0](command);
            }
            else
            {
                throw new InvalidOperationException("no handler registered");
            }
        }
    }
}
