namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModifiedAtToLayawayItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LayawayItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LayawayItems", "ModifiedAt");
        }
    }
}
