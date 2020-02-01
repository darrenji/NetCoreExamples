using Autofac;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Application.Configuration.Data;
using TotalNetCore.DDDAPISample.Domain.Customers;
using TotalNetCore.DDDAPISample.Domain.Interfaces;
using TotalNetCore.DDDAPISample.Domain.Payments;
using TotalNetCore.DDDAPISample.Domain.Products;
using TotalNetCore.DDDAPISample.Infrastructure.Domain;
using TotalNetCore.DDDAPISample.Infrastructure.Domain.Customers;
using TotalNetCore.DDDAPISample.Infrastructure.Domain.Payments;
using TotalNetCore.DDDAPISample.Infrastructure.Domain.Products;
using TotalNetCore.DDDAPISample.Infrastructure.Shared;

namespace TotalNetCore.DDDAPISample.Infrastructure.Database
{
    public class DataAccessModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;

        public DataAccessModule(string databaseConnectionString)
        {
            this._databaseConnectionString = databaseConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();


            builder.RegisterType<CustomerRepository>()
                .As<ICustomerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PaymentRepository>()
                .As<IPaymentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StronglyTypedIdValueConverterSelector>()
                .As<IValueConverterSelector>()
                .InstancePerLifetimeScope();
        }
    }
}
