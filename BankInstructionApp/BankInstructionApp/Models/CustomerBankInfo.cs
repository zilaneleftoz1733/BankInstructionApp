using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BankInstructionApp.Models
{
    public class CustomerBankInfo
    {
        [Key]
        public int CustomerBankInfoId { get; set; }

        [Required(ErrorMessage = "Cari Kodu zorunludur.")]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Cari Adı zorunludur.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Banka Adı zorunludur.")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Şube Adı zorunludur.")]
        public string BranchName { get; set; }

        [Required(ErrorMessage = "IBAN No zorunludur.")]
        public string IBAN { get; set; }

        [Display(Name = "Vergi No")]
        public string TaxNumber { get; set; }

        [Display(Name = "Para Birimi")]
        public string Currency { get; set; }
		public virtual ICollection<InstructionViewModel> Instructions { get; set; }


	}
}
