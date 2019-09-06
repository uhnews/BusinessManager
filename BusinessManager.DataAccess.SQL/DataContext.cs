using BusinessManager.Core.Models;
using System.Data.Entity;

namespace BusinessManager.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext() 
            : base("DefaultConnection") {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<POSTransaction> POSTransactions { get; set; }
        public DbSet<POSTransactionItem> POSTransactionItems { get; set; }
        public DbSet<POSSale> POSSales { get; set; }
        public DbSet<POSSaleItem> POSSaleItems { get; set; }
    }
}