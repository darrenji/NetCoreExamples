using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDCafe.Lib.CQRS
{
    public interface IHandleCommand<TCommand>
    {
        IEnumerable<Event> Handle(TCommand command);
    }
}
