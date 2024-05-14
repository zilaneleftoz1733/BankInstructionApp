namespace BankInstructionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.CustomerBankInfoes", "TaxNumber", c => c.String());
            DropColumn("dbo.Customers", "TaxNumber");

			CreateTable(
	"dbo.OperationTypes",
	c => new
	{
		OperationTypeID = c.Int(nullable: false, identity: true),
		BankOperationType = c.String(),
		Description = c.String(),
	})
	.PrimaryKey(t => t.OperationTypeID);

		}

		public override void Down()
        {
            AddColumn("dbo.Customers", "TaxNumber", c => c.String());
            DropColumn("dbo.CustomerBankInfoes", "TaxNumber");
            DropTable("dbo.Users");
        }
    }
}
