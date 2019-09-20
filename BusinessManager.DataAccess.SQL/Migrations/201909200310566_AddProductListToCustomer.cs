namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductListToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Customer_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Products", "Customer_Id");
            AddForeignKey("dbo.Products", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Products", new[] { "Customer_Id" });
            DropColumn("dbo.Products", "Customer_Id");
        }
    }
}
