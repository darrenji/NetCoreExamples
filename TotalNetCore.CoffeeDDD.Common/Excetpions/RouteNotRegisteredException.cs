using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public class RouteNotRegisteredException : Exception
    {
        public RouteNotRegisteredException(Type messageType) : base(string.Format("No route specified for message '{0}'", messageType.FullName))
        {
        }
    }
}
