namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveModifiedAtFrom3Tables : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BasketItems", "ModifiedAt");
            DropColumn("dbo.LayawayItems", "ModifiedAt");
            DropColumn("dbo.POSTransactionItems", "ModifiedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.POSTransactionItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.LayawayItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.BasketItems", "ModifiedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
