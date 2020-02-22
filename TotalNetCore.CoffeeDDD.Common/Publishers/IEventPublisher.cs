using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : Event;
    }
}
