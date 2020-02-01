using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Application.Orders.ChangeCustomerOrder
{
    public class ChangeCustomerOrderCommand : IRequest
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public string Currency { get; }

        public List<ProductDto> Products { get; }

        public ChangeCustomerOrderCommand(
            Guid customerId,
            Guid orderId,
            List<ProductDto> products,
            string currency)
        {
            this.CustomerId = customerId;
            this.OrderId = orderId;
            this.Currency = currency;
            this.Products = products;
        }
    }
}
