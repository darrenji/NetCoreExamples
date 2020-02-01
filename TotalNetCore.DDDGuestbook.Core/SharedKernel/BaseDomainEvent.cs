using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDGuestbook.Core.SharedKernel
{
    public abstract class BaseDomainEvent
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
