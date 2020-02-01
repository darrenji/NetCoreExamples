using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Application.Orders;

namespace TotalNetCore.DDDAPISample.API.Customers
{
    public class CustomerOrderRequest
    {
        public List<ProductDto> Products { get; set; }

        public string Currency { get; set; }
    }
}
