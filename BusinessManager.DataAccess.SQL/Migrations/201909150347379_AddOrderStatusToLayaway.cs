namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderStatusToLayaway : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Layaways", "OrderStatus", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Layaways", "OrderStatus");
        }
    }
}
