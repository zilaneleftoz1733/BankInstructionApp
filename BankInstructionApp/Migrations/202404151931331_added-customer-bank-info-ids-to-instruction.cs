namespace BankInstructionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcustomerbankinfoidstoinstruction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InstructionViewModels", "CustomerBankInfoIds", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InstructionViewModels", "CustomerBankInfoIds");
        }
    }
}
