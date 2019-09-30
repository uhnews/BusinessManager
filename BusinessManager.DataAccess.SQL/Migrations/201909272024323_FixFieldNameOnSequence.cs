namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixFieldNameOnSequence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sequences", "SequenceNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Sequences", "SquenceNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sequences", "SquenceNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Sequences", "SequenceNumber");
        }
    }
}
