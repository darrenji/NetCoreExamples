using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.AutofacExample.Web.EventsDynamically
{
    /// <summary>
    /// 处理事件接口
    /// </summary>
    public interface IHandler<in T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}
