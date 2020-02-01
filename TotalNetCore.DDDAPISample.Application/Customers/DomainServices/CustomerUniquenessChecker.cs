using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Application.Configuration.Data;
using TotalNetCore.DDDAPISample.Domain.Customers;

namespace TotalNetCore.DDDAPISample.Application.Customers.DomainServices
{
    public class CustomerUniquenessChecker : ICustomerUniquenessChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public CustomerUniquenessChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public bool IsUnique(string customerEmail)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT TOP 1 1" +
                               "FROM [orders].[Customers] AS [Customer] " +
                               "WHERE [Customer].[Email] = @Email";
            var customersNumber = connection.QuerySingleOrDefault<int?>(sql,
                            new
                            {
                                Email = customerEmail
                            });

            return !customersNumber.HasValue;
        }
    }
}
