﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDAPISample.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommand : IRequest
    {
        public Guid CustomerId { get; }

        public List<ProductDto> Products { get; }

        public string Currency { get; }

        public PlaceCustomerOrderCommand(
            Guid customerId,
            List<ProductDto> products,
            string currency)
        {
            this.CustomerId = customerId;
            this.Products = products;
            this.Currency = currency;
        }
    }
}
