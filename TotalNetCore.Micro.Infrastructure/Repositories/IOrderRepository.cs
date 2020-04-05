using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.Micro.Domain.OrderAggregate;
using TotalNetCore.Micro.InfrastructureCore;

namespace TotalNetCore.Micro.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order, long>
    {
    }
}
