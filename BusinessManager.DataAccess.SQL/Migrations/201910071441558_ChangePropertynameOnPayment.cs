namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePropertynameOnPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "CreditCardCVV", c => c.String());
            DropColumn("dbo.Payments", "SecurityCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "SecurityCode", c => c.String());
            DropColumn("dbo.Payments", "CreditCardCVV");
        }
    }
}
