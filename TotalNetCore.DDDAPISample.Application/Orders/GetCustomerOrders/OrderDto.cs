using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Application.Orders.GetCustomerOrders
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }

        public bool IsRemoved { get; set; }
    }
}
