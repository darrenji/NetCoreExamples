using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : Message
    {
        void Handle(TEvent message);
    }
}
