using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDCafe.Lib.CQRS
{
    public interface ISubscribeTo<TEvent> where TEvent:Event
    {
        void Hanle(TEvent e);
    }
}
