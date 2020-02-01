using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TotalNetCore.DDDAPISample.Infrastructure.Processing
{
    /// <summary>
    /// 发送领域事件的接口，它的实现是DomainEventsDispatcher
    /// </summary>
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
