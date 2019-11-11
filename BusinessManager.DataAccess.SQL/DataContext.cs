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
        public DbSet<OnlineOrder> Orders { get; set; }
        public DbSet<OnlineOrderItem> OnlineOrderItems { get; set; }
        public DbSet<POSTransaction> POSTransactions { get; set; }
        public DbSet<POSTransactionItem> POSTransactionItems { get; set; }
        public DbSet<POSSale> POSSales { get; set; }
        public DbSet<POSSaleItem> POSSaleItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Layaway> Layaways { get; set; }
        public DbSet<LayawayItem> LayawayItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CustomerNote> CustomerNotes { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Sequence> Sequences { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //}
    }
}