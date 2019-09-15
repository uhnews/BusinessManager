namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignIndexForCustomerRelatedModels : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Invoices", "CustomerId");
            CreateIndex("dbo.Layaways", "CustomerId");
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Layaways", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Layaways", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Layaways", new[] { "CustomerId" });
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
        }
    }
}
