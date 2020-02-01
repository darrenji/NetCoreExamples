using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Domain.Customers;
using TotalNetCore.DDDAPISample.Domain.Customers.Orders;
using TotalNetCore.DDDAPISample.Domain.ForeignExchange;
using TotalNetCore.DDDAPISample.Domain.Products;

namespace TotalNetCore.DDDAPISample.Application.Orders.ChangeCustomerOrder
{
    internal class ChangeCustomerOrderCommandHandler : IRequestHandler<ChangeCustomerOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IForeignExchange _foreignExchange;

        public ChangeCustomerOrderCommandHandler(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IForeignExchange foreignExchange)
        {
            this._customerRepository = customerRepository;
            this._productRepository = productRepository;
            _foreignExchange = foreignExchange;
        }

        public async Task<Unit> Handle(ChangeCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            var orderId = new OrderId(request.OrderId);

            var allProducts = await this._productRepository.GetAllAsync();

            var conversionRates = this._foreignExchange.GetConversionRates();
            var orderProducts = request
                    .Products
                    .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity))
                    .ToList();

            customer.ChangeOrder(
                orderId,
                allProducts,
                orderProducts,
                conversionRates,
                request.Currency);

            return Unit.Value;
        }
    }
}
