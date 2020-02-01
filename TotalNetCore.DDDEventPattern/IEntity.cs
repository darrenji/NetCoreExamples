using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDEventPattern
{
    public interface IEntity
    {
        IEnumerable<IEvent> Events { get; }
    }
}
