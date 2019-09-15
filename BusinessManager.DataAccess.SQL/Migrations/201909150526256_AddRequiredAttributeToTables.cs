namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredAttributeToTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Layaways", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.LayawayItems", "LayawayId", "dbo.Layaways");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("dbo.Layaways", new[] { "CustomerId" });
            DropIndex("dbo.LayawayItems", new[] { "LayawayId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            AlterColumn("dbo.BasketItems", "BasketId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BasketItems", "ProductId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Invoices", "CustomerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Invoices", "PayerFirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Invoices", "PayerLastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.InvoiceItems", "InvoiceId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.InvoiceItems", "ProductId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.InvoiceItems", "ProductName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Layaways", "CustomerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.LayawayItems", "LayawayId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.LayawayItems", "ProductId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.LayawayItems", "ProductName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Orders", "CustomerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Orders", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Orders", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.OrderItems", "OrderId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.OrderItems", "ProductId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.OrderItems", "ProductName", c => c.String(nullable: false, maxLength: 60));
            CreateIndex("dbo.BasketItems", "BasketId");
            CreateIndex("dbo.Invoices", "CustomerId");
            CreateIndex("dbo.InvoiceItems", "InvoiceId");
            CreateIndex("dbo.Layaways", "CustomerId");
            CreateIndex("dbo.LayawayItems", "LayawayId");
            CreateIndex("dbo.Orders", "CustomerId");
            CreateIndex("dbo.OrderItems", "OrderId");
            AddForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Layaways", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LayawayItems", "LayawayId", "dbo.Layaways", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.LayawayItems", "LayawayId", "dbo.Layaways");
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Layaways", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.LayawayItems", new[] { "LayawayId" });
            DropIndex("dbo.Layaways", new[] { "CustomerId" });
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            AlterColumn("dbo.OrderItems", "ProductName", c => c.String(maxLength: 60));
            AlterColumn("dbo.OrderItems", "ProductId", c => c.String(maxLength: 128));
            AlterColumn("dbo.OrderItems", "OrderId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Orders", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.Orders", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Orders", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Orders", "CustomerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.LayawayItems", "ProductName", c => c.String(maxLength: 50));
            AlterColumn("dbo.LayawayItems", "ProductId", c => c.String(maxLength: 128));
            AlterColumn("dbo.LayawayItems", "LayawayId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Layaways", "CustomerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.InvoiceItems", "ProductName", c => c.String(maxLength: 50));
            AlterColumn("dbo.InvoiceItems", "ProductId", c => c.String(maxLength: 128));
            AlterColumn("dbo.InvoiceItems", "InvoiceId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Invoices", "PayerLastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Invoices", "PayerFirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Invoices", "CustomerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.BasketItems", "ProductId", c => c.String(maxLength: 128));
            AlterColumn("dbo.BasketItems", "BasketId", c => c.String(maxLength: 128));
            CreateIndex("dbo.OrderItems", "OrderId");
            CreateIndex("dbo.Orders", "CustomerId");
            CreateIndex("dbo.LayawayItems", "LayawayId");
            CreateIndex("dbo.Layaways", "CustomerId");
            CreateIndex("dbo.InvoiceItems", "InvoiceId");
            CreateIndex("dbo.Invoices", "CustomerId");
            CreateIndex("dbo.BasketItems", "BasketId");
            AddForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders", "Id");
            AddForeignKey("dbo.LayawayItems", "LayawayId", "dbo.Layaways", "Id");
            AddForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices", "Id");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Layaways", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets", "Id");
        }
    }
}
