namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncMinorChangesRequired : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Invoice_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Payments", "Invoice_Id");
            AddForeignKey("dbo.Payments", "Invoice_Id", "dbo.Invoices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "Invoice_Id", "dbo.Invoices");
            DropIndex("dbo.Payments", new[] { "Invoice_Id" });
            DropColumn("dbo.Payments", "Invoice_Id");
        }
    }
}
