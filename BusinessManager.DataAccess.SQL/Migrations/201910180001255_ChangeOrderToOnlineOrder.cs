namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrderToOnlineOrder : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Orders", newName: "OnlineOrders");
            DropForeignKey("dbo.OnlineOrderItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.OnlineOrderItems", new[] { "OrderId" });
            AddColumn("dbo.OnlineOrderItems", "OnlineOrder_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.OnlineOrderItems", "OnlineOrder_Id");
            AddForeignKey("dbo.OnlineOrderItems", "OnlineOrder_Id", "dbo.OnlineOrders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OnlineOrderItems", "OnlineOrder_Id", "dbo.OnlineOrders");
            DropIndex("dbo.OnlineOrderItems", new[] { "OnlineOrder_Id" });
            DropColumn("dbo.OnlineOrderItems", "OnlineOrder_Id");
            CreateIndex("dbo.OnlineOrderItems", "OrderId");
            AddForeignKey("dbo.OnlineOrderItems", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.OnlineOrders", newName: "Orders");
        }
    }
}
