using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Domain.Customers;
using TotalNetCore.DDDAPISample.Infrastructure.Database;
using TotalNetCore.DDDAPISample.Infrastructure.Shared;

namespace TotalNetCore.DDDAPISample.Infrastructure.Domain.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrdersContext _context;

        public CustomerRepository(OrdersContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Customer customer)
        {
            await this._context.Customers.AddAsync(customer);
        }

        public async Task<Customer> GetByIdAsync(CustomerId id)
        {
            return await this._context.Customers
                .IncludePaths(
                    CustomerEntityTypeConfiguration.OrdersList,
                    CustomerEntityTypeConfiguration.OrderProducts)
                .SingleAsync(x => x.Id == id);
        }
    }
}
