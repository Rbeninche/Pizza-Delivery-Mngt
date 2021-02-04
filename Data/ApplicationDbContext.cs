using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaDeliveryManagement.Models;

namespace PizzaDeliveryManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salary { get; set; }

        public DbSet<PizzaSize> PizzaSize { get; set; }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<OrderCustomerInfo> OrderCustomerInfo { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PizzaSize>().HasData(
                new PizzaSize { PizzaSizeId = "S", Size = "Small" },
                new PizzaSize { PizzaSizeId = "M", Size = "Medium" },
                new PizzaSize { PizzaSizeId = "L", Size = "Large" }

                );

            modelBuilder.Entity<Menu>().HasData(
                new Menu { MenuId = 1, PizzaName = "Cheese Pizza", Price = 5, PizzaSizeId = "S" },
                new Menu { MenuId = 2, PizzaName = "Cheese Pizza", Price = 7, PizzaSizeId = "M" },
                new Menu { MenuId = 3, PizzaName = "Cheese Pizza", Price = 9, PizzaSizeId = "L" },
                new Menu { MenuId = 4, PizzaName = "Veggie Pizza", Price = 6, PizzaSizeId = "S" },
                new Menu { MenuId = 5, PizzaName = "Veggie Pizza", Price = 8, PizzaSizeId = "M" },
                new Menu { MenuId = 6, PizzaName = "Veggie Pizza", Price = 10, PizzaSizeId = "L" },
                new Menu { MenuId = 7, PizzaName = "Pepperoni Pizza", Price = 7, PizzaSizeId = "S" },
                new Menu { MenuId = 8, PizzaName = "Pepperoni Pizza", Price = 9, PizzaSizeId = "M" },
                new Menu { MenuId = 9, PizzaName = "Pepperoni Pizza", Price = 11, PizzaSizeId = "L" }

                );
        }
    }
}
