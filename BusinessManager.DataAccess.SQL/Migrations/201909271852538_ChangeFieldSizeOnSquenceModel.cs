namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFieldSizeOnSquenceModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sequences", "SequenceName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sequences", "SequenceName", c => c.String(nullable: false));
        }
    }
}
