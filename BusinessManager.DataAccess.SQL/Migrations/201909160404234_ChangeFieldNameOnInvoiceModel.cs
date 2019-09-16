namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFieldNameOnInvoiceModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "PayerStreet", c => c.String(maxLength: 50));
            DropColumn("dbo.Invoices", "PayerAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "PayerAddress", c => c.String(maxLength: 50));
            DropColumn("dbo.Invoices", "PayerStreet");
        }
    }
}
