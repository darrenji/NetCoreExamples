﻿using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Application.Configuration.Data;

namespace TotalNetCore.DDDAPISample.Application.Orders.GetCustomerOrders
{
    internal class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, List<OrderDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetCustomerOrdersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<OrderDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT " +
                               "[Order].[Id], " +
                               "[Order].[IsRemoved], " +
                               "[Order].[Value], " +
                               "[Order].[Currency] " +
                               "FROM orders.v_Orders AS [Order] " +
                               "WHERE [Order].CustomerId = @CustomerId";
            var orders = await connection.QueryAsync<OrderDto>(sql, new { request.CustomerId });

            return orders.AsList();
        }
    }
}
