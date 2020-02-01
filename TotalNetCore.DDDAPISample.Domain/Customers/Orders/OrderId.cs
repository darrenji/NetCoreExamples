using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Shared;

namespace TotalNetCore.DDDAPISample.Domain.Customers.Orders
{
   public  class OrderId : TypedIdValueBase
    {
        public OrderId(Guid value) : base(value)
        {

        }
    }
}
