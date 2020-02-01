using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Domain.Customers.Orders
{
    public enum OrderStatus
    {
        Placed = 0,
        InRealization = 1,
        Canceled =2,
        Delivered=3,
        Sent=4,
        WaitingForPayment =5
    }
}
