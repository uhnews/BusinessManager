namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestoreOnlineOrderItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OnlineOrderItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OnlineOrderId = c.String(nullable: false, maxLength: 128),
                        ProductId = c.String(nullable: false, maxLength: 128),
                        ProductName = c.String(nullable: false, maxLength: 60),
                        ProductDescription = c.String(nullable: false, maxLength: 70),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(maxLength: 200),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OnlineOrders", t => t.OnlineOrderId, cascadeDelete: true)
                .Index(t => t.OnlineOrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OnlineOrderItems", "OnlineOrderId", "dbo.OnlineOrders");
            DropIndex("dbo.OnlineOrderItems", new[] { "OnlineOrderId" });
            DropTable("dbo.OnlineOrderItems");
        }
    }
}
