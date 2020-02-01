﻿using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Application.Configuration.Data;

namespace TotalNetCore.DDDAPISample.Application.Orders.GetCustomerOrderDetails
{
    internal class GetCustomerOrderDetialsQueryHandler : IRequestHandler<GetCustomerOrderDetailsQuery, OrderDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetCustomerOrderDetialsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<OrderDetailsDto> Handle(GetCustomerOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[Order].[Id], " +
                               "[Order].[IsRemoved], " +
                               "[Order].[Value], " +
                               "[Order].[Currency] " +
                               "FROM orders.v_Orders AS [Order] " +
                               "WHERE [Order].Id = @OrderId";
            var order = await connection.QuerySingleOrDefaultAsync<OrderDetailsDto>(sql, new { request.OrderId });

            const string sqlProducts = "SELECT " +
                               "[Order].[ProductId] AS [Id], " +
                               "[Order].[Quantity], " +
                               "[Order].[Name], " +
                               "[Order].[Value], " +
                               "[Order].[Currency] " +
                               "FROM orders.v_OrderProducts AS [Order] " +
                               "WHERE [Order].OrderId = @OrderId";
            var products = await connection.QueryAsync<ProductDto>(sqlProducts, new { request.OrderId });

            order.Products = products.AsList();

            return order;
        }
    }
}
