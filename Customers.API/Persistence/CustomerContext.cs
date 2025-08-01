﻿using Microsoft.EntityFrameworkCore;

namespace Customers.API.Persistence
{
    public class CustomerContext:DbContext
    {   
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }

        public DbSet<Entities.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.Customer>().ToTable("Customers");
            modelBuilder.Entity<Entities.Customer>().HasIndex(c => c.UserName).IsUnique();
            modelBuilder.Entity<Entities.Customer>().HasIndex(c => c.Email).IsUnique();

        }
    }
}
