namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSquenceModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sequences",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SequenceName = c.String(),
                        SquenceNumber = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sequences");
        }
    }
}
