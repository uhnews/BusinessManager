namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestRemoveBasketIdRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BasketItems", "ProductId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BasketItems", "ProductId", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
