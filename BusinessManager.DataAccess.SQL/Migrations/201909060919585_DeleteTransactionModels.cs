namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTransactionModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.POSTransactionItems", "POSTransaction_Id", "dbo.POSTransactions");
            DropIndex("dbo.POSTransactionItems", new[] { "POSTransaction_Id" });
            DropTable("dbo.POSTransactionItems");
            DropTable("dbo.POSTransactions");
        }
        
        public override void Down()
        {
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
            
            CreateTable(
                "dbo.POSTransactionItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TransactionId = c.String(),
                        ProductId = c.String(),
                        ProductName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        POSTransaction_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.POSTransactionItems", "POSTransaction_Id");
            AddForeignKey("dbo.POSTransactionItems", "POSTransaction_Id", "dbo.POSTransactions", "Id");
        }
    }
}
