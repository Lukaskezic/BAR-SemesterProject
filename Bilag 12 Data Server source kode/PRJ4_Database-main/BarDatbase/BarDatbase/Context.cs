using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarDatabase.Models;
using Microsoft.EntityFrameworkCore;


namespace BarDatabase
{
    public class Context : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Bartender> Bartenders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BarTest; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Context()
        {
        }

        public Context(string cs)
        {
            _connectionString = cs;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            ob.UseSqlServer(_connectionString);
        }

        // Setup function for migration
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Set Uniques
            mb.Entity<Bartender>()
                .HasIndex(b => b.Username)
                .IsUnique();
            mb.Entity<Category>()
                .HasIndex(c => c.Type)
                .IsUnique();
            mb.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            // Set default values
            mb.Entity<Bartender>()
                .Property(b => b.IsAdmin)
                .HasDefaultValue(false);
            mb.Entity<Order>()
                .Property(o => o.CreationTime)
                .HasDefaultValueSql("GETDATE()");
            mb.Entity<Product>()
                .Property(p => p.ImgUrl)
                .HasDefaultValue("https://upload.wikimedia.org/wikipedia/commons/e/e4/Comic_image_missing.svg");

            // Setup complicated Many-to-Many relationships
            mb.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithMany(p => p.Products)
                .UsingEntity<ProductOrder>(
                    j => j
                        .HasOne(po => po.Order)
                        .WithMany(o => o.ProductOrders)
                        .HasForeignKey(po => po.OrderId),
                    j => j
                        .HasOne(po => po.Product)
                        .WithMany(p => p.ProductOrders)
                        .HasForeignKey(po => po.ProductId),
                    j =>
                    {
                        j.Property(po => po.Amount);
                        j.HasKey(pt => new { pt.OrderId, pt.ProductId });
                    });
            mb.Entity<Product>()
                .HasMany(p => p.Ingredients)
                .WithMany(p => p.Products)
                .UsingEntity<ProductIngredient>(
                    j => j
                        .HasOne(pi => pi.Ingredient)
                        .WithMany(o => o.ProductIngredients)
                        .HasForeignKey(pi => pi.IngredientId),
                    j => j
                        .HasOne(pi => pi.Product)
                        .WithMany(p => p.ProductIngredients)
                        .HasForeignKey(pi => pi.ProductId),
                    j =>
                    {
                        j.Property(pi => pi.Amount);
                        j.Property(pi => pi.MeasurementType);
                        j.HasKey(pt => new { pt.IngredientId, pt.ProductId });
                    });

        }
    }
}
