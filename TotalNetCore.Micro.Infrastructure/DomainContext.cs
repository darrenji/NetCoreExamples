using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.Micro.Domain.OrderAggregate;
using TotalNetCore.Micro.Domain.UserAggregate;
using TotalNetCore.Micro.Infrastructure.EntityConfigurations;
using TotalNetCore.Micro.InfrastructureCore;

namespace TotalNetCore.Micro.Infrastructure
{
    public class DomainContext : EFContext
    {
        public DomainContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
