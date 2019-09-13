namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModifitedAtToItemsTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.POSTransactionItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.POSTransactionItems", "ModifiedAt");
            DropColumn("dbo.BasketItems", "ModifiedAt");
        }
    }
}
