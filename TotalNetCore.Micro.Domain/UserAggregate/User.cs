using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.Micro.DomainAbstraction;

namespace TotalNetCore.Micro.Domain.UserAggregate
{
    public class User : Entity<long>, IAggregateRoot
    {
    }
}
