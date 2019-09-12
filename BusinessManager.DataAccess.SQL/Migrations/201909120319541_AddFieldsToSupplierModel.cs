namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsToSupplierModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Suppliers", "BusinessDescription", c => c.String(maxLength: 200));
            AddColumn("dbo.Suppliers", "BusinessCategory", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "BusinessCategory");
            DropColumn("dbo.Suppliers", "BusinessDescription");
        }
    }
}
