﻿using Ardalis.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotalNetCore.DDDGuestbook.Core.Entities;
using TotalNetCore.DDDGuestbook.Core.Interfaces;
using TotalNetCore.DDDGuestbook.Core.SharedKernel;

namespace TotalNetCore.DDDGuestbook.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher dispatcher)
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        public DbSet<Guestbook> Guestbooks { get; set; }
        public DbSet<GuestbookEntry> GuestbookEntries { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

            var navigation = modelBuilder.Entity<Guestbook>()
                .Metadata.FindNavigation(nameof(Guestbook.Entries));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            // alternately this is built-in to EF Core 2.2
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            int result = base.SaveChanges();

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    //把事件分发出去
                    _dispatcher.Dispatch(domainEvent);
                }
            }

            return result;
        }

    }
}
