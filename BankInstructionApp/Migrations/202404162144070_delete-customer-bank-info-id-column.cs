namespace BankInstructionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletecustomerbankinfoidcolumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InstructionViewModels", "CustomerBankInfoId", "dbo.CustomerBankInfoes");
            DropIndex("dbo.InstructionViewModels", new[] { "CustomerBankInfoId" });
            RenameColumn(table: "dbo.InstructionViewModels", name: "CustomerBankInfoId", newName: "CustomerBankInfo_CustomerBankInfoId");
            AlterColumn("dbo.InstructionViewModels", "CustomerBankInfo_CustomerBankInfoId", c => c.Int());
            AlterColumn("dbo.InstructionViewModels", "CustomerBankInfoIds", c => c.String(nullable: false));
            CreateIndex("dbo.InstructionViewModels", "CustomerBankInfo_CustomerBankInfoId");
            AddForeignKey("dbo.InstructionViewModels", "CustomerBankInfo_CustomerBankInfoId", "dbo.CustomerBankInfoes", "CustomerBankInfoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InstructionViewModels", "CustomerBankInfo_CustomerBankInfoId", "dbo.CustomerBankInfoes");
            DropIndex("dbo.InstructionViewModels", new[] { "CustomerBankInfo_CustomerBankInfoId" });
            AlterColumn("dbo.InstructionViewModels", "CustomerBankInfoIds", c => c.String());
            AlterColumn("dbo.InstructionViewModels", "CustomerBankInfo_CustomerBankInfoId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.InstructionViewModels", name: "CustomerBankInfo_CustomerBankInfoId", newName: "CustomerBankInfoId");
            CreateIndex("dbo.InstructionViewModels", "CustomerBankInfoId");
            AddForeignKey("dbo.InstructionViewModels", "CustomerBankInfoId", "dbo.CustomerBankInfoes", "CustomerBankInfoId", cascadeDelete: true);
        }
    }
}
