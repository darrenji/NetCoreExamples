using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Application.Orders.GetCustomerOrderDetails
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }

        public bool IsRemoved { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
