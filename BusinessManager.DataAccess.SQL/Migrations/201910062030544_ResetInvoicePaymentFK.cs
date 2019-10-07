namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetInvoicePaymentFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "Invoice_Id", "dbo.Invoices");
            DropIndex("dbo.Payments", new[] { "Invoice_Id" });
            DropColumn("dbo.Payments", "ReceivableSourceId");
            RenameColumn(table: "dbo.Payments", name: "Invoice_Id", newName: "ReceivableSourceId");
            AlterColumn("dbo.Payments", "ReceivableSourceId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Payments", "ReceivableSourceId");
            AddForeignKey("dbo.Payments", "ReceivableSourceId", "dbo.Invoices", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "ReceivableSourceId", "dbo.Invoices");
            DropIndex("dbo.Payments", new[] { "ReceivableSourceId" });
            AlterColumn("dbo.Payments", "ReceivableSourceId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.Payments", name: "ReceivableSourceId", newName: "Invoice_Id");
            AddColumn("dbo.Payments", "ReceivableSourceId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Payments", "Invoice_Id");
            AddForeignKey("dbo.Payments", "Invoice_Id", "dbo.Invoices", "Id");
        }
    }
}
