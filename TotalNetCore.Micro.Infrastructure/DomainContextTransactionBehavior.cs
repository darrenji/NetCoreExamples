using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.Micro.InfrastructureCore.Behaviors;

namespace TotalNetCore.Micro.Infrastructure
{
    public class DomainContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<DomainContext, TRequest, TResponse>
    {
        public DomainContextTransactionBehavior(DomainContext dbContext, ICapPublisher capBus, ILogger<DomainContextTransactionBehavior<TRequest, TResponse>> logger) : base(dbContext, capBus, logger)
        {
        }
    }
}
