namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRequiredForSupplier : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Suppliers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Suppliers", "LastName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suppliers", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Suppliers", "FirstName", c => c.String(maxLength: 50));
        }
    }
}
