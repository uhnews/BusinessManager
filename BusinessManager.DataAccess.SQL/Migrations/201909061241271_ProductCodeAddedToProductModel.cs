namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductCodeAddedToProductModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.Products", "UPC", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "UPC", c => c.String());
            DropColumn("dbo.Products", "ProductCode");
        }
    }
}
