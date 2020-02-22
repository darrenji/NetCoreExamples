using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.DomainModel.Ddd
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }
    }
}
