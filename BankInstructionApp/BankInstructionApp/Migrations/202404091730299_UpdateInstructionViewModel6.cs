namespace BankInstructionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInstructionViewModel6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.InstructionViewModels", "CustomerBankInfoIds");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InstructionViewModels", "CustomerBankInfoIds", c => c.Int(nullable: false));
        }
    }
}
