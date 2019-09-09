namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldLengthsToAllModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        POSSale_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.POSSales", t => t.POSSale_Id)
                .Index(t => t.POSSale_Id);
            
            AddColumn("dbo.Orders", "CompanyName", c => c.String(maxLength: 65));
            AddColumn("dbo.POSSales", "CompanyName", c => c.String(maxLength: 65));
            AddColumn("dbo.POSTransactions", "CompanyName", c => c.String(maxLength: 65));
            AlterColumn("dbo.BasketItems", "ProductId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Customers", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Customers", "CompanyName", c => c.String(maxLength: 65));
            AlterColumn("dbo.Customers", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "Street", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "City", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "State", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "ZipCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "Phone", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "Website", c => c.String(maxLength: 65));
            AlterColumn("dbo.OrderItems", "ProductId", c => c.String(maxLength: 128));
            AlterColumn("dbo.OrderItems", "ProductName", c => c.String(maxLength: 60));
            AlterColumn("dbo.OrderItems", "Image", c => c.String(maxLength: 200));
            AlterColumn("dbo.Orders", "CustomerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Orders", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Orders", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Orders", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.Orders", "Street", c => c.String(maxLength: 50));
            AlterColumn("dbo.Orders", "City", c => c.String(maxLength: 50));
            AlterColumn("dbo.Orders", "State", c => c.String(maxLength: 50));
            AlterColumn("dbo.Orders", "ZipCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.Orders", "OrderStatus", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSSaleItems", "ProductId", c => c.String(maxLength: 128));
            AlterColumn("dbo.POSSaleItems", "Image", c => c.String(maxLength: 200));
            AlterColumn("dbo.POSSales", "CustomerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.POSSales", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSSales", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSSales", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSSales", "Street", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSSales", "City", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSSales", "State", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSSales", "ZipCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSTransactionItems", "ProductId", c => c.String(maxLength: 128));
            AlterColumn("dbo.POSTransactionItems", "ProductName", c => c.String(maxLength: 60));
            AlterColumn("dbo.POSTransactionItems", "Image", c => c.String(maxLength: 200));
            AlterColumn("dbo.POSTransactions", "CustomerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.POSTransactions", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSTransactions", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSTransactions", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSTransactions", "Street", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSTransactions", "City", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSTransactions", "State", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSTransactions", "ZipCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.POSTransactions", "OrderStatus", c => c.String(maxLength: 50));
            AlterColumn("dbo.ProductCategories", "Category", c => c.String(maxLength: 50));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 70));
            AlterColumn("dbo.Products", "Category", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Image", c => c.String(maxLength: 200));
            AlterColumn("dbo.Suppliers", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Suppliers", "CompanyName", c => c.String(maxLength: 65));
            AlterColumn("dbo.Suppliers", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Suppliers", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Suppliers", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.Suppliers", "Street", c => c.String(maxLength: 50));
            AlterColumn("dbo.Suppliers", "City", c => c.String(maxLength: 50));
            AlterColumn("dbo.Suppliers", "State", c => c.String(maxLength: 50));
            AlterColumn("dbo.Suppliers", "ZipCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.Suppliers", "Phone", c => c.String(maxLength: 50));
            AlterColumn("dbo.Suppliers", "Website", c => c.String(maxLength: 65));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerViewModels", "POSSale_Id", "dbo.POSSales");
            DropIndex("dbo.CustomerViewModels", new[] { "POSSale_Id" });
            AlterColumn("dbo.Suppliers", "Website", c => c.String());
            AlterColumn("dbo.Suppliers", "Phone", c => c.String());
            AlterColumn("dbo.Suppliers", "ZipCode", c => c.String());
            AlterColumn("dbo.Suppliers", "State", c => c.String());
            AlterColumn("dbo.Suppliers", "City", c => c.String());
            AlterColumn("dbo.Suppliers", "Street", c => c.String());
            AlterColumn("dbo.Suppliers", "Email", c => c.String());
            AlterColumn("dbo.Suppliers", "LastName", c => c.String());
            AlterColumn("dbo.Suppliers", "FirstName", c => c.String());
            AlterColumn("dbo.Suppliers", "CompanyName", c => c.String());
            AlterColumn("dbo.Suppliers", "UserId", c => c.String());
            AlterColumn("dbo.Products", "Image", c => c.String());
            AlterColumn("dbo.Products", "Category", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.ProductCategories", "Category", c => c.String());
            AlterColumn("dbo.POSTransactions", "OrderStatus", c => c.String());
            AlterColumn("dbo.POSTransactions", "ZipCode", c => c.String());
            AlterColumn("dbo.POSTransactions", "State", c => c.String());
            AlterColumn("dbo.POSTransactions", "City", c => c.String());
            AlterColumn("dbo.POSTransactions", "Street", c => c.String());
            AlterColumn("dbo.POSTransactions", "Email", c => c.String());
            AlterColumn("dbo.POSTransactions", "LastName", c => c.String());
            AlterColumn("dbo.POSTransactions", "FirstName", c => c.String());
            AlterColumn("dbo.POSTransactions", "CustomerId", c => c.String());
            AlterColumn("dbo.POSTransactionItems", "Image", c => c.String());
            AlterColumn("dbo.POSTransactionItems", "ProductName", c => c.String());
            AlterColumn("dbo.POSTransactionItems", "ProductId", c => c.String());
            AlterColumn("dbo.POSSales", "ZipCode", c => c.String());
            AlterColumn("dbo.POSSales", "State", c => c.String());
            AlterColumn("dbo.POSSales", "City", c => c.String());
            AlterColumn("dbo.POSSales", "Street", c => c.String());
            AlterColumn("dbo.POSSales", "Email", c => c.String());
            AlterColumn("dbo.POSSales", "LastName", c => c.String());
            AlterColumn("dbo.POSSales", "FirstName", c => c.String());
            AlterColumn("dbo.POSSales", "CustomerId", c => c.String());
            AlterColumn("dbo.POSSaleItems", "Image", c => c.String());
            AlterColumn("dbo.POSSaleItems", "ProductId", c => c.String());
            AlterColumn("dbo.Orders", "OrderStatus", c => c.String());
            AlterColumn("dbo.Orders", "ZipCode", c => c.String());
            AlterColumn("dbo.Orders", "State", c => c.String());
            AlterColumn("dbo.Orders", "City", c => c.String());
            AlterColumn("dbo.Orders", "Street", c => c.String());
            AlterColumn("dbo.Orders", "Email", c => c.String());
            AlterColumn("dbo.Orders", "LastName", c => c.String());
            AlterColumn("dbo.Orders", "FirstName", c => c.String());
            AlterColumn("dbo.Orders", "CustomerId", c => c.String());
            AlterColumn("dbo.OrderItems", "Image", c => c.String());
            AlterColumn("dbo.OrderItems", "ProductName", c => c.String());
            AlterColumn("dbo.OrderItems", "ProductId", c => c.String());
            AlterColumn("dbo.Customers", "Website", c => c.String());
            AlterColumn("dbo.Customers", "Phone", c => c.String());
            AlterColumn("dbo.Customers", "ZipCode", c => c.String());
            AlterColumn("dbo.Customers", "State", c => c.String());
            AlterColumn("dbo.Customers", "City", c => c.String());
            AlterColumn("dbo.Customers", "Street", c => c.String());
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "LastName", c => c.String());
            AlterColumn("dbo.Customers", "FirstName", c => c.String());
            AlterColumn("dbo.Customers", "CompanyName", c => c.String());
            AlterColumn("dbo.Customers", "UserId", c => c.String());
            AlterColumn("dbo.BasketItems", "ProductId", c => c.String());
            DropColumn("dbo.POSTransactions", "CompanyName");
            DropColumn("dbo.POSSales", "CompanyName");
            DropColumn("dbo.Orders", "CompanyName");
            DropTable("dbo.CustomerViewModels");
        }
    }
}
