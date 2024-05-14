using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BankInstructionApp.Models
{
    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }

        [Required(ErrorMessage = "Banka Adı zorunludur.")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Şube Adı zorunludur.")]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "IBAN No zorunludur.")]
        public string IBAN { get; set; }

        [Display(Name = "Para Birimi")]
        public string Currency { get; set; }

        public string Description { get; set; }
		public virtual ICollection<InstructionViewModel> Instructions { get; set; }

	}
}