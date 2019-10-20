namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryChangeOrderIdToOnlineOrderId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OnlineOrderItems", "OnlineOrderId", "dbo.OnlineOrders");
            DropIndex("dbo.OnlineOrderItems", new[] { "OnlineOrderId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.OnlineOrderItems", "OnlineOrderId");
            AddForeignKey("dbo.OnlineOrderItems", "OnlineOrderId", "dbo.OnlineOrders", "Id", cascadeDelete: true);
        }
    }
}
