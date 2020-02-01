using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Domain.Interfaces
{
    /// <summary>
    /// 领域事件接口，实现MediatorR的INotfication接口
    /// </summary>
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
