namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsToBasketItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "ProductName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.BasketItems", "ProductDescription", c => c.String(nullable: false, maxLength: 70));
            AddColumn("dbo.BasketItems", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BasketItems", "Image", c => c.String(maxLength: 200));
            AlterColumn("dbo.BasketItems", "ProductId", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BasketItems", "ProductId", c => c.String(maxLength: 128));
            DropColumn("dbo.BasketItems", "Image");
            DropColumn("dbo.BasketItems", "Price");
            DropColumn("dbo.BasketItems", "ProductDescription");
            DropColumn("dbo.BasketItems", "ProductName");
        }
    }
}
