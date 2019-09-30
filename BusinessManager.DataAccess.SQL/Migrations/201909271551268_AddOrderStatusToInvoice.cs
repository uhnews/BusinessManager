namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderStatusToInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "OrderStatus", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "OrderStatus");
        }
    }
}
