using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace BankInstructionApp.Models
{
	public class InstructionViewModel
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Banka hesabı seçiniz.")]
		[Display(Name = "Banka Hesabı")]
		public int? BankAccountId { get; set; }

		public string CustomerBankInfoIds { get; set; }

		[Required(ErrorMessage = "İşlem türü seçiniz.")]
		[Display(Name = "İşlem Türü")]
		public int? operationTypeID { get; set; }
		public List<SelectListItem> YourBankAccountList { get; set; }
		public List<SelectListItem> YourCustomerBankInfoList { get; set; }
		public List<SelectListItem> YourOperationTypeList { get; set; }

	}

}

