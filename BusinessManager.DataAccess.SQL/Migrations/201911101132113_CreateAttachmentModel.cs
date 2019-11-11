namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAttachmentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 250),
                        Type = c.String(maxLength: 50),
                        Category = c.String(maxLength: 50),
                        FileName = c.String(maxLength: 50),
                        Location = c.String(maxLength: 100),
                        AttachedBy = c.String(nullable: false, maxLength: 50),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachments", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Attachments", new[] { "CustomerId" });
            DropTable("dbo.Attachments");
        }
    }
}
