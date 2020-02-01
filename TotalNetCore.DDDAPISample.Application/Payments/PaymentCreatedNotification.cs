using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Application.Configuration.DomainEvents;
using TotalNetCore.DDDAPISample.Domain.Payments;

namespace TotalNetCore.DDDAPISample.Application.Payments
{
    public class PaymentCreatedNotification : DomainNotificationBase<TotalNetCore.DDDAPISample.Domain.Payments.PaymentCreatedEvent>
    {
        public PaymentId PaymentId { get; }

        public PaymentCreatedNotification(PaymentCreatedEvent domainEvent) : base(domainEvent)
        {
            this.PaymentId = domainEvent.PaymentId;
        }

        [JsonConstructor]
        public PaymentCreatedNotification(PaymentId paymentId) : base(null)
        {
            this.PaymentId = paymentId;
        }
    }
}
