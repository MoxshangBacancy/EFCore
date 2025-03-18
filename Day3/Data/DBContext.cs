using System.Collections.Generic;
using System.Reflection.Emit;
using Day_3_Task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Day_3_Task.Data
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
               new Customer { Id = 1, Name = "John Doe", Email = "john@example.com", PhoneNumber = "123-456-7890", CreatedDate = new DateTime(2024, 3, 1, 12, 0, 0) },
               new Customer { Id = 2, Name = "Jane Smith", Email = "jane@example.com", PhoneNumber = "987-654-3210", CreatedDate = new DateTime(2024, 3, 1, 12, 0, 0) }
           );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 1200.99m, Stock = 50 },
                new Product { Id = 2, Name = "Phone", Price = 800.49m, Stock = 100 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, OrderDate = new DateTime(2024, 3, 1, 12, 0, 0), CustomerId = 1, IsDeleted = false },
                new Order { Id = 2, OrderDate = new DateTime(2024, 3, 1, 12, 0, 0), CustomerId = 2, IsDeleted = false }
            );

            modelBuilder.Entity<OrderProduct>().HasData(
                new OrderProduct { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1 },
                new OrderProduct { Id = 2, OrderId = 2, ProductId = 2, Quantity = 2 }
            );


            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            
        }



    }
}
