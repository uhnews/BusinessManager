namespace BusinessManager.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertiesToPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "CreditCardExpMonth", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "CreditCardExpYear", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "SecurityCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "SecurityCode");
            DropColumn("dbo.Payments", "CreditCardExpYear");
            DropColumn("dbo.Payments", "CreditCardExpMonth");
        }
    }
}
