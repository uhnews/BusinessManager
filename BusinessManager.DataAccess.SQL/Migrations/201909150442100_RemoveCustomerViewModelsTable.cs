namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCustomerViewModelsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerViewModels", "POSSale_Id", "dbo.POSSales");
            DropIndex("dbo.CustomerViewModels", new[] { "POSSale_Id" });
            DropTable("dbo.CustomerViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustomerViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        POSSale_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.CustomerViewModels", "POSSale_Id");
            AddForeignKey("dbo.CustomerViewModels", "POSSale_Id", "dbo.POSSales", "Id");
        }
    }
}
