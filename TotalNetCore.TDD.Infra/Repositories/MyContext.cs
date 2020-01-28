using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.TDD.Infra.Models;

namespace TotalNetCore.TDD.Infra.Repositories
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(e=> {
                e.ToTable("user")
                .HasKey(t => t.Id);

                e.Property(t => t.Id)
                .ValueGeneratedOnAdd();
            });
        }
    }
}
