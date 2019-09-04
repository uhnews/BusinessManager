namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerIdToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CustomerId");
        }
    }
}
