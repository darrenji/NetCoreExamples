using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public interface IRouteMessages
    {
        void Route(object message);
    }
}
