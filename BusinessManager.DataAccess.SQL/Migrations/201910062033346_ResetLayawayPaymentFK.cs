namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetLayawayPaymentFK : DbMigration
    {
        public override void Up()
        {
            //AddForeignKey("dbo.Payments", "ReceivableSourceId", "dbo.Layaways", "Id", cascadeDelete: true);
            DropForeignKey("dbo.Payments", "ReceivableSourceId", "dbo.Layaways");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "ReceivableSourceId", "dbo.Layaways");
        }
    }
}
