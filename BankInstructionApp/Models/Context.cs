using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankInstructionApp.Models
{
    public class Context: DbContext
    {
      
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerBankInfo> CustomerBankInfos { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
		public DbSet<InstructionViewModel> InstructionViewModels { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			
			modelBuilder.Ignore<SelectListItem>();
		}
	}
}