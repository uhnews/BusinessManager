namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLengthFromInvoiceNo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "InvoiceNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "InvoiceNumber", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
