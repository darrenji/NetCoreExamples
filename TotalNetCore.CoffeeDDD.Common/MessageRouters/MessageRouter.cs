using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common.MessageRouters
{
    public class MessageRouter : IRouteMessages
    {
        private readonly IDictionary<Type, ICollection<Action<object>>> _routes;

        public MessageRouter()
        {
            _routes = new Dictionary<Type, ICollection<Action<object>>>();
        }

        public void Register<TMessage>(Action<TMessage> route) where TMessage : class
        {
            var routeKey = typeof(TMessage);
            ICollection<Action<object>> routes;

            if(!_routes.TryGetValue(routeKey, out routes))
            {
                _routes[routeKey] = routes = new LinkedList<Action<object>>();
            }

            routes.Add(message => route(message as TMessage));

            _routes.Add(routeKey, routes);
        }

        public void Route(object message)
        {
            ICollection<Action<object>> routes;

            if(!_routes.TryGetValue(message.GetType(),out routes))
            {
                throw new RouteNotRegisteredException(message.GetType());
            }

            foreach (var route in routes)
                route(message);
        }
    }
}
