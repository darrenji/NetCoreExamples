using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.AutofacExample.Web.EventsDynamically
{
    public interface IEntity
    {
        List<IDomainEvent> Events { get; }
    }
}
