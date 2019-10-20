namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteOnlineOderItem : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.OnlineOrderItems");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
        }
    }
}
