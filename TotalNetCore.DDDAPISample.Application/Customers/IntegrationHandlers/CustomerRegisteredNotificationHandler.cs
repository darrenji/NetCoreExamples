using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Application.Configuration.Processing;

namespace TotalNetCore.DDDAPISample.Application.Customers.IntegrationHandlers
{
    /// <summary>
    /// CustomerRegisteredNotification是具体的Domain Notofication Object
    /// INotificationHandler是对CustomerRegisteredNotification的订阅
    /// </summary>
    public class CustomerRegisteredNotificationHandler : INotificationHandler<CustomerRegisteredNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public CustomerRegisteredNotificationHandler(
            ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }
        public async Task Handle(CustomerRegisteredNotification notification, CancellationToken cancellationToken)
        {
            // Send welcome e-mail message...

            await this._commandsScheduler.EnqueueAsync(new MarkCustomerAsWelcomedCommand(notification.CustomerId));
        }
    }
}
