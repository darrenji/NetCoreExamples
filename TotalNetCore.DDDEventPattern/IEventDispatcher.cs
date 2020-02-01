using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TotalNetCore.DDDEventPattern
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<T>(params T[] events) where T : IEvent;
    }
}
