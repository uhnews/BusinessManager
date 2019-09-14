namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFiveNewModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        InvoiceId = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        ProductName = c.String(maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        InvoiceNo = c.String(nullable: false, maxLength: 50),
                        CustomerId = c.String(maxLength: 128),
                        InvoiceDate = c.DateTimeOffset(nullable: false, precision: 7),
                        PayerFirstName = c.String(maxLength: 50),
                        PayerLastName = c.String(maxLength: 50),
                        PayerCompany = c.String(maxLength: 50),
                        PayerAddress = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        State = c.String(maxLength: 50),
                        ZipCode = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 50),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LayawayItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LayawayId = c.String(maxLength: 128),
                        ProductId = c.String(maxLength: 128),
                        ProductName = c.String(maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Layaways", t => t.LayawayId)
                .Index(t => t.LayawayId);
            
            CreateTable(
                "dbo.Layaways",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.String(maxLength: 128),
                        AgreementDate = c.DateTimeOffset(nullable: false, precision: 7),
                        DueDate = c.DateTimeOffset(nullable: false, precision: 7),
                        DownPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServiceFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CancellationFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PaymentDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentMode = c.String(nullable: false, maxLength: 50),
                        CheckNo = c.String(maxLength: 50),
                        CheckImage = c.String(maxLength: 200),
                        CheckWriter = c.String(maxLength: 50),
                        CreditCardHolder = c.String(maxLength: 50),
                        CreditCardNo = c.String(maxLength: 50),
                        CreditCardName = c.String(maxLength: 30),
                        ReceivableSource = c.String(nullable: false, maxLength: 50),
                        ReceivableSourceId = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LayawayItems", "LayawayId", "dbo.Layaways");
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.LayawayItems", new[] { "LayawayId" });
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropTable("dbo.Payments");
            DropTable("dbo.Layaways");
            DropTable("dbo.LayawayItems");
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceItems");
        }
    }
}
