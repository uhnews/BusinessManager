namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POSSaleModelsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POSSaleItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        POSSaleId = c.String(maxLength: 128),
                        ProductId = c.String(),
                        ProductName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.POSSales", t => t.POSSaleId)
                .Index(t => t.POSSaleId);
            
            CreateTable(
                "dbo.POSSales",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POSSaleItems", "POSSaleId", "dbo.POSSales");
            DropIndex("dbo.POSSaleItems", new[] { "POSSaleId" });
            DropTable("dbo.POSSales");
            DropTable("dbo.POSSaleItems");
        }
    }
}
