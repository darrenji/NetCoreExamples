using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDGuestbook.Core.SharedKernel;

namespace TotalNetCore.DDDGuestbook.Core.Interfaces
{
    /// <summary>
    /// 把事件分发出去
    /// </summary>
    public interface IDomainEventDispatcher
    {
        void Dispatch(BaseDomainEvent domainEvent);
    }
}
