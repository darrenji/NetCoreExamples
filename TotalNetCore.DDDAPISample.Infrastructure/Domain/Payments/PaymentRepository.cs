using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Domain.Payments;
using TotalNetCore.DDDAPISample.Infrastructure.Database;

namespace TotalNetCore.DDDAPISample.Infrastructure.Domain.Payments
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly OrdersContext _context;

        public PaymentRepository(OrdersContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Payment> GetByIdAsync(PaymentId id)
        {
            return await this._context.Payments
                .SingleAsync(x => x.Id == id);
        }

        public async Task AddAsync(Payment payment)
        {
            await this._context.Payments.AddAsync(payment);
        }
    }
}
