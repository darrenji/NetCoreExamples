using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Domain.Payments;

namespace TotalNetCore.DDDAPISample.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedDomainEventHandler : INotificationHandler<TotalNetCore.DDDAPISample.Domain.Customers.Orders.OrderEvents.OrderPlacedEvent>
    {
        private readonly IPaymentRepository _paymentRepository;

        public OrderPlacedDomainEventHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task Handle(TotalNetCore.DDDAPISample.Domain.Customers.Orders.OrderEvents.OrderPlacedEvent notification, CancellationToken cancellationToken)
        {
            var newPayment = new Payment(notification.OrderId);

            await this._paymentRepository.AddAsync(newPayment);
        }
    }
}
