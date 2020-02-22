using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.DomainModel.Ddd
{
    public abstract class DomainEvent
    {
        public Guid Id { get; protected set; }
        public DateTime OccuredOn { get; protected set; }

        protected DomainEvent(Guid id, DateTime occuredOn)
        {
            Id = id;
            OccuredOn = occuredOn;
        }

        protected DomainEvent()
        {
            Id = Guid.NewGuid();
            OccuredOn = SysTime.Now();
        }

    }
}
