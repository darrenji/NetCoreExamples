using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.SimpleCQRS.Lib
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : Event;
    }
}
