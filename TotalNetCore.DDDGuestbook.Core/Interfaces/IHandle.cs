using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDGuestbook.Core.SharedKernel;

namespace TotalNetCore.DDDGuestbook.Core.Interfaces
{
    /// <summary>
    /// 处理事件的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHandle<T> where T:BaseDomainEvent
    {
        void Handle(T domainEvent);
    }
}
