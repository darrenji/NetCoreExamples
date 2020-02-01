using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TotalNetCore.DDDEventPattern
{
    public class EventDispatcher : IEventDispatcher
    {
        public Task DispatchAsync<T>(params T[] events) where T : IEvent
        {
            throw new NotImplementedException();
        }
    }
}
