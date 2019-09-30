namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredToSquenceModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sequences", "SequenceName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sequences", "SequenceName", c => c.String());
        }
    }
}
