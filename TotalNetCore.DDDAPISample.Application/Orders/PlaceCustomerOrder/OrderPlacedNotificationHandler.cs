using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TotalNetCore.DDDAPISample.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedNotificationHandler : INotificationHandler<OrderPlacedNotification>
    {
        public async Task Handle(OrderPlacedNotification request, CancellationToken cancellationToken)
        {
            // Send email.
        }
    }
}
