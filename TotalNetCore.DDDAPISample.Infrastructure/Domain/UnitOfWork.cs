using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Domain.Interfaces;
using TotalNetCore.DDDAPISample.Infrastructure.Database;
using TotalNetCore.DDDAPISample.Infrastructure.Processing;

namespace TotalNetCore.DDDAPISample.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrdersContext _ordersContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            OrdersContext ordersContext,
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            this._ordersContext = ordersContext;
            this._domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this._domainEventsDispatcher.DispatchEventsAsync();//把领域的事件发送出去，是Outbox Pattern的写法
            return await this._ordersContext.SaveChangesAsync(cancellationToken);
        }
    }
}
