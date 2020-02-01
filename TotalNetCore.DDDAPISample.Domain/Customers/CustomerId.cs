using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Shared;

namespace TotalNetCore.DDDAPISample.Domain.Customers
{
    public class CustomerId : TypedIdValueBase
    {

        public CustomerId(Guid value) : base(value)
        {

        }
    }
}
