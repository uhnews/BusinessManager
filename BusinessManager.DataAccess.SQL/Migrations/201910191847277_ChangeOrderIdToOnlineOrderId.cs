namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrderIdToOnlineOrderId : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.OnlineOrderItems", "OnlineOrder_Id", "dbo.OnlineOrders");
            //DropIndex("dbo.OnlineOrderItems", new[] { "OnlineOrder_Id" });
            //RenameColumn(table: "dbo.OnlineOrderItems", name: "OnlineOrder_Id", newName: "OnlineOrderId");
            //AlterColumn("dbo.OnlineOrderItems", "OnlineOrderId", c => c.String(nullable: false, maxLength: 128));
            //CreateIndex("dbo.OnlineOrderItems", "OnlineOrderId");
            //AddForeignKey("dbo.OnlineOrderItems", "OnlineOrderId", "dbo.OnlineOrders", "Id", cascadeDelete: true);
            //DropColumn("dbo.OnlineOrderItems", "OrderId");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.OnlineOrderItems", "OrderId", c => c.String(nullable: false, maxLength: 128));
            //DropForeignKey("dbo.OnlineOrderItems", "OnlineOrderId", "dbo.OnlineOrders");
            //DropIndex("dbo.OnlineOrderItems", new[] { "OnlineOrderId" });
            //AlterColumn("dbo.OnlineOrderItems", "OnlineOrderId", c => c.String(maxLength: 128));
            //RenameColumn(table: "dbo.OnlineOrderItems", name: "OnlineOrderId", newName: "OnlineOrder_Id");
            //CreateIndex("dbo.OnlineOrderItems", "OnlineOrder_Id");
            //AddForeignKey("dbo.OnlineOrderItems", "OnlineOrder_Id", "dbo.OnlineOrders", "Id");
        }
    }
}
