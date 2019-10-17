namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductDescptionToPOSSaleItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POSSaleItems", "ProductDescription", c => c.String(nullable: false, maxLength: 70));
        }
        
        public override void Down()
        {
            DropColumn("dbo.POSSaleItems", "ProductDescription");
        }
    }
}
