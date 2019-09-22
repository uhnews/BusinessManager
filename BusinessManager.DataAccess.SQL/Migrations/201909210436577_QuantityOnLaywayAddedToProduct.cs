namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuantityOnLaywayAddedToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "QuantityOnLayaway", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "QuantityOnLayaway");
        }
    }
}
