namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModifiedAtToBaseEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Baskets", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Customers", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Invoices", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.InvoiceItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Payments", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Layaways", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.LayawayItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Orders", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.OrderItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.POSSaleItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.POSSales", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.POSTransactionItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.POSTransactions", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.ProductCategories", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Products", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Suppliers", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "ModifiedAt");
            DropColumn("dbo.Products", "ModifiedAt");
            DropColumn("dbo.ProductCategories", "ModifiedAt");
            DropColumn("dbo.POSTransactions", "ModifiedAt");
            DropColumn("dbo.POSTransactionItems", "ModifiedAt");
            DropColumn("dbo.POSSales", "ModifiedAt");
            DropColumn("dbo.POSSaleItems", "ModifiedAt");
            DropColumn("dbo.OrderItems", "ModifiedAt");
            DropColumn("dbo.Orders", "ModifiedAt");
            DropColumn("dbo.LayawayItems", "ModifiedAt");
            DropColumn("dbo.Layaways", "ModifiedAt");
            DropColumn("dbo.Payments", "ModifiedAt");
            DropColumn("dbo.InvoiceItems", "ModifiedAt");
            DropColumn("dbo.Invoices", "ModifiedAt");
            DropColumn("dbo.Customers", "ModifiedAt");
            DropColumn("dbo.Baskets", "ModifiedAt");
            DropColumn("dbo.BasketItems", "ModifiedAt");
        }
    }
}
