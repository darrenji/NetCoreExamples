using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Application.Configuration.Emails;
using TotalNetCore.DDDAPISample.Domain.Payments;

namespace TotalNetCore.DDDAPISample.Application.Payments.SendEmailAfterPayment
{
    public class SendEmailAfterPaymentCommandHandler : IRequestHandler<SendEmailAfterPaymentCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IPaymentRepository _paymentRepository;

        public SendEmailAfterPaymentCommandHandler(
            IEmailSender emailSender,
            IPaymentRepository paymentRepository)
        {
            _emailSender = emailSender;
            _paymentRepository = paymentRepository;
        }

        public async Task<Unit> Handle(SendEmailAfterPaymentCommand request, CancellationToken cancellationToken)
        {
            // Logic of preparing an email. This is only mock.
            var emailMessage = new EmailMessage("from@email.com", "to@email.com", "content");

            await _emailSender.SendEmailAsync(emailMessage);

            var payment = await this._paymentRepository.GetByIdAsync(request.PaymentId);

            payment.MarkEmailNotificationIsSent();

            return Unit.Value;
        }
    }
}
