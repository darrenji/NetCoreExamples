using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDCafe.Lib.CQRS
{
    public interface IApplyEvent<TEvent> where TEvent : Event
    {
        void Apply(TEvent e);
    }
}
