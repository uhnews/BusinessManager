namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionToLayawayItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LayawayItems", "ProductDescription", c => c.String(nullable: false, maxLength: 70));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LayawayItems", "ProductDescription");
        }
    }
}
