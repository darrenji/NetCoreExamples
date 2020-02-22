using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.CoffeeDDD.Common
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot, new()
    {
        void Save(TAggregateRoot aggregate, int expectedVersion);
        TAggregateRoot GetById(Guid id);
    }
}
