using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Application.Orders
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }



        public string Name { get; set; }
    }
}
