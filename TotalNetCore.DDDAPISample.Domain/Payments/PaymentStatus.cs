using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Domain.Payments
{
    public enum PaymentStatus
    {
        ToPay = 0,
        Payed = 1,
        Overdue = 2
    }
}
