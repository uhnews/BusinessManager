namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrderItemToOnlineOrderItem : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OrderItems", newName: "OnlineOrderItems");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.OnlineOrderItems", newName: "OrderItems");
        }
    }
}
