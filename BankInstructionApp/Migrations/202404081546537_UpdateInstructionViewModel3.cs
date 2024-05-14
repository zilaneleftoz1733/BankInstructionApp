namespace BankInstructionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInstructionViewModel3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InstructionViewModels", "CustomerBankInfo_CustomerBankInfoId", "dbo.CustomerBankInfoes");
            DropIndex("dbo.InstructionViewModels", new[] { "CustomerBankInfo_CustomerBankInfoId" });
            RenameColumn(table: "dbo.InstructionViewModels", name: "CustomerBankInfo_CustomerBankInfoId", newName: "CustomerBankInfoId");
            AlterColumn("dbo.InstructionViewModels", "CustomerBankInfoId", c => c.Int(nullable: false));
            CreateIndex("dbo.InstructionViewModels", "CustomerBankInfoId");
            AddForeignKey("dbo.InstructionViewModels", "CustomerBankInfoId", "dbo.CustomerBankInfoes", "CustomerBankInfoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InstructionViewModels", "CustomerBankInfoId", "dbo.CustomerBankInfoes");
            DropIndex("dbo.InstructionViewModels", new[] { "CustomerBankInfoId" });
            AlterColumn("dbo.InstructionViewModels", "CustomerBankInfoId", c => c.Int());
            RenameColumn(table: "dbo.InstructionViewModels", name: "CustomerBankInfoId", newName: "CustomerBankInfo_CustomerBankInfoId");
            CreateIndex("dbo.InstructionViewModels", "CustomerBankInfo_CustomerBankInfoId");
            AddForeignKey("dbo.InstructionViewModels", "CustomerBankInfo_CustomerBankInfoId", "dbo.CustomerBankInfoes", "CustomerBankInfoId");
        }
    }
}
