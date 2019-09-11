namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSummaryFielsToSale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POSSales", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.POSSales", "TotalItemCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.POSSales", "TotalItemCount");
            DropColumn("dbo.POSSales", "TotalAmount");
        }
    }
}
