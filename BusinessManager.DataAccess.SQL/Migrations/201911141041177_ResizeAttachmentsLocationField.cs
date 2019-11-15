namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResizeAttachmentsLocationField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attachments", "Location", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attachments", "Location", c => c.String(maxLength: 100));
        }
    }
}
