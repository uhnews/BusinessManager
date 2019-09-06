namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POSTransactionsAdded2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POSTransactionItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        POSTransactionId = c.String(maxLength: 128),
                        ProductId = c.String(),
                        ProductName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.POSTransactions", t => t.POSTransactionId)
                .Index(t => t.POSTransactionId);
            
            CreateTable(
                "dbo.POSTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        OrderStatus = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POSTransactionItems", "POSTransactionId", "dbo.POSTransactions");
            DropIndex("dbo.POSTransactionItems", new[] { "POSTransactionId" });
            DropTable("dbo.POSTransactions");
            DropTable("dbo.POSTransactionItems");
        }
    }
}
