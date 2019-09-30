namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInvoiceNoToInvoiceNumberOnInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "InvoiceNumber", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Invoices", "InvoiceNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "InvoiceNo", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Invoices", "InvoiceNumber");
        }
    }
}
