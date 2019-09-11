namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Phone2", c => c.String(maxLength: 50));
            AddColumn("dbo.Orders", "Phone", c => c.String(maxLength: 50));
            AddColumn("dbo.POSSales", "Phone", c => c.String(maxLength: 50));
            AddColumn("dbo.POSTransactions", "Phone", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.POSTransactions", "Phone");
            DropColumn("dbo.POSSales", "Phone");
            DropColumn("dbo.Orders", "Phone");
            DropColumn("dbo.Customers", "Phone2");
        }
    }
}
