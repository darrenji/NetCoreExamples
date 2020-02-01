using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Application.Customers.RegisterCustomer
{
    public class RegisterCustomerRequest
    {
        public string Email { get; set; }

        public string Name { get; set; }
    }
}
