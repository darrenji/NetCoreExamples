using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Web.Infrastructure.MessageQueue
{
    public class RabbitMqEventPublisher : IEventPublisher
    {
        private readonly IBus bus;

        public RabbitMqEventPublisher(IBus bus)
        {
            this.bus = bus;
        }
        public void Publish(DomainEvent @event)
        {
            bus.Publish(@event);
        }
    }
}
