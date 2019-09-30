namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToInvoiceItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceItems", "Image", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceItems", "Image");
        }
    }
}
