using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Products;

namespace TotalNetCore.DDDAPISample.Domain.Customers.Orders
{
    public class OrderProductData
    {
        public ProductId ProductId { get; }
        public int Quantity { get; }
        public OrderProductData(ProductId productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
