namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageFieldToLayawayItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LayawayItems", "Image", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LayawayItems", "Image");
        }
    }
}
