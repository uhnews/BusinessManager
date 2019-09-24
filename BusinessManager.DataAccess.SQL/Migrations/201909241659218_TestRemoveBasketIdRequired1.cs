namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestRemoveBasketIdRequired1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            AlterColumn("dbo.BasketItems", "BasketId", c => c.String(maxLength: 128));
            AlterColumn("dbo.BasketItems", "ProductId", c => c.String(maxLength: 128));
            CreateIndex("dbo.BasketItems", "BasketId");
            AddForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            AlterColumn("dbo.BasketItems", "ProductId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BasketItems", "BasketId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BasketItems", "BasketId");
            AddForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets", "Id", cascadeDelete: true);
        }
    }
}
