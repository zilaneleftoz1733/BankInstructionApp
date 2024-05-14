namespace BankInstructionApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInstructionViewModelProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InstructionViewModels", "YourBankAccountList_DataGroupField", c => c.String());
            AddColumn("dbo.InstructionViewModels", "YourBankAccountList_DataTextField", c => c.String());
            AddColumn("dbo.InstructionViewModels", "YourBankAccountList_DataValueField", c => c.String());
            AddColumn("dbo.InstructionViewModels", "YourCustomerBankInfoList_DataGroupField", c => c.String());
            AddColumn("dbo.InstructionViewModels", "YourCustomerBankInfoList_DataTextField", c => c.String());
            AddColumn("dbo.InstructionViewModels", "YourCustomerBankInfoList_DataValueField", c => c.String());
            AddColumn("dbo.InstructionViewModels", "YourOperationTypeList_DataGroupField", c => c.String());
            AddColumn("dbo.InstructionViewModels", "YourOperationTypeList_DataTextField", c => c.String());
            AddColumn("dbo.InstructionViewModels", "YourOperationTypeList_DataValueField", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InstructionViewModels", "YourOperationTypeList_DataValueField");
            DropColumn("dbo.InstructionViewModels", "YourOperationTypeList_DataTextField");
            DropColumn("dbo.InstructionViewModels", "YourOperationTypeList_DataGroupField");
            DropColumn("dbo.InstructionViewModels", "YourCustomerBankInfoList_DataValueField");
            DropColumn("dbo.InstructionViewModels", "YourCustomerBankInfoList_DataTextField");
            DropColumn("dbo.InstructionViewModels", "YourCustomerBankInfoList_DataGroupField");
            DropColumn("dbo.InstructionViewModels", "YourBankAccountList_DataValueField");
            DropColumn("dbo.InstructionViewModels", "YourBankAccountList_DataTextField");
            DropColumn("dbo.InstructionViewModels", "YourBankAccountList_DataGroupField");
        }
    }
}
