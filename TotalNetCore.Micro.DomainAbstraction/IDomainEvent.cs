using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.Micro.DomainAbstraction
{
    /// <summary>
    /// 需要被发布出去
    /// </summary>
    public interface IDomainEvent : INotification
    {
    }
}
