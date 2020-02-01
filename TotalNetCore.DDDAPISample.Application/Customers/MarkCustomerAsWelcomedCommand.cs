using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Customers;

namespace TotalNetCore.DDDAPISample.Application.Customers
{
    public class MarkCustomerAsWelcomedCommand : IRequest
    {
        public MarkCustomerAsWelcomedCommand(CustomerId customerId)
        {
            CustomerId = customerId;
        }

        public CustomerId CustomerId { get; }
    }
}
