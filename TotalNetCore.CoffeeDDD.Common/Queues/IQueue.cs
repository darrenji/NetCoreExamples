using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public interface IQueue
    {
        void Put(object item);
        void Pop(Action<object> popAction);
    }
}
