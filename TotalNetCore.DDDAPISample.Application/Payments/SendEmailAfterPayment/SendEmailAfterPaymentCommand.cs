using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Payments;

namespace TotalNetCore.DDDAPISample.Application.Payments.SendEmailAfterPayment
{
    public class SendEmailAfterPaymentCommand : IRequest
    {
        public PaymentId PaymentId { get; }

        public SendEmailAfterPaymentCommand(PaymentId paymentId)
        {
            this.PaymentId = paymentId;
        }
    }
}
