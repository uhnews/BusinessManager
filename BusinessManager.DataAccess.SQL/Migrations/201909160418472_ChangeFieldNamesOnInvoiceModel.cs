namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFieldNamesOnInvoiceModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "PayerCity", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "PayerState", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "PayerZipCode", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "PayerEmail", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "PayerPhone", c => c.String(maxLength: 50));
            DropColumn("dbo.Invoices", "City");
            DropColumn("dbo.Invoices", "State");
            DropColumn("dbo.Invoices", "ZipCode");
            DropColumn("dbo.Invoices", "Email");
            DropColumn("dbo.Invoices", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "Phone", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "Email", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "ZipCode", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "State", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoices", "City", c => c.String(maxLength: 50));
            DropColumn("dbo.Invoices", "PayerPhone");
            DropColumn("dbo.Invoices", "PayerEmail");
            DropColumn("dbo.Invoices", "PayerZipCode");
            DropColumn("dbo.Invoices", "PayerState");
            DropColumn("dbo.Invoices", "PayerCity");
        }
    }
}
