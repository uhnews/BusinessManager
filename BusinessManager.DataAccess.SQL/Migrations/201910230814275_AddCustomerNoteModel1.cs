namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerNoteModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerNotes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Category = c.String(maxLength: 50),
                        NoteBody = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerNotes");
        }
    }
}
