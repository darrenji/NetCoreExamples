﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDAPISample.Domain.Products;
using TotalNetCore.DDDAPISample.Infrastructure.Database;

namespace TotalNetCore.DDDAPISample.Infrastructure.Domain.Products
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", SchemaNames.Orders);

            builder.HasKey(b => b.Id);

            builder.OwnsMany<ProductPrice>("_prices", y =>
            {
                y.ToTable("ProductPrices", SchemaNames.Orders);
                y.Property<ProductId>("ProductId");
                y.Property<string>("Currency").HasColumnType("varchar(3)").IsRequired();
                y.HasKey("ProductId", "Currency");
                y.OwnsOne(p => p.Value, mv =>
                {
                    mv.Property(p => p.Currency).HasColumnName("Currency").HasColumnType("varchar(3)").IsRequired();
                    mv.Property(p => p.Value).HasColumnName("Value");
                });
            });
        }
    }
}
