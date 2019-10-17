namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductDescriptionToOrderItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "ProductDescription", c => c.String(nullable: false, maxLength: 70));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "ProductDescription");
        }
    }
}
