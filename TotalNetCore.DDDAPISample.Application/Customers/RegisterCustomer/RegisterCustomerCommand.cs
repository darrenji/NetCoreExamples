using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Application.Customers.RegisterCustomer
{
    public class RegisterCustomerCommand : IRequest<CustomerDto>
    {
        public string Email { get; }

        public string Name { get; }

        public RegisterCustomerCommand(string email, string name)
        {
            this.Email = email;
            this.Name = name;
        }
    }
}
