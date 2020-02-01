using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Customers.Orders;
using TotalNetCore.DDDAPISample.Domain.Interfaces;
using TotalNetCore.DDDAPISample.Domain.Shared;

namespace TotalNetCore.DDDAPISample.Domain.Payments
{
    public class Payment : Entity, IAggregateRoot
    {
        public PaymentId Id { get; private set; }

        private OrderId _orderId;

        private DateTime _createDate;

        private PaymentStatus _status;

        private bool _emailNotificationIsSent;

        private Payment()
        {
            // Only for EF.
        }

        public Payment(OrderId orderId)
        {
            this.Id = new PaymentId(Guid.NewGuid());
            this._createDate = DateTime.UtcNow;
            this._orderId = orderId;
            this._status = PaymentStatus.ToPay;
            this._emailNotificationIsSent = false;

            this.AddDomainEvent(new PaymentCreatedEvent(this.Id, this._orderId));
        }

        public void MarkEmailNotificationIsSent()
        {
            this._emailNotificationIsSent = true;
        }
    }
}
