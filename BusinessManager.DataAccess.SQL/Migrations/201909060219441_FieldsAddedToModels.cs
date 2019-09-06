namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldsAddedToModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SupplierPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "WholesalePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "QuantityMin", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "UPC", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "UPC");
            DropColumn("dbo.Products", "QuantityMin");
            DropColumn("dbo.Products", "WholesalePrice");
            DropColumn("dbo.Products", "SupplierPrice");
        }
    }
}
