using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Adapter
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}
