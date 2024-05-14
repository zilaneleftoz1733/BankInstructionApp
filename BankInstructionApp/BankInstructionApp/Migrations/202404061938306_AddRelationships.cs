namespace BankInstructionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationships : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InstructionViewModels", "OperationType_opreationID", c => c.Int());
            CreateIndex("dbo.InstructionViewModels", "BankAccountId");
            CreateIndex("dbo.InstructionViewModels", "CustomerBankInfoId");
            CreateIndex("dbo.InstructionViewModels", "OperationType_opreationID");
            AddForeignKey("dbo.InstructionViewModels", "BankAccountId", "dbo.BankAccounts", "BankAccountId", cascadeDelete: true);
            AddForeignKey("dbo.InstructionViewModels", "CustomerBankInfoId", "dbo.CustomerBankInfoes", "CustomerBankInfoId", cascadeDelete: true);
            AddForeignKey("dbo.InstructionViewModels", "OperationType_opreationID", "dbo.OperationTypes", "opreationID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InstructionViewModels", "OperationType_opreationID", "dbo.OperationTypes");
            DropForeignKey("dbo.InstructionViewModels", "CustomerBankInfoId", "dbo.CustomerBankInfoes");
            DropForeignKey("dbo.InstructionViewModels", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.InstructionViewModels", new[] { "OperationType_opreationID" });
            DropIndex("dbo.InstructionViewModels", new[] { "CustomerBankInfoId" });
            DropIndex("dbo.InstructionViewModels", new[] { "BankAccountId" });
            DropColumn("dbo.InstructionViewModels", "OperationType_opreationID");
        }
    }
}
