using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BankInstructionApp.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Cari Kodu zorunludur.")]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Cari Adı zorunludur.")]
        public string CustomerName { get; set; }

        public string Address { get; set; }

        [Display(Name = "Vergi Dairesi")]
        public string TaxOffice { get; set; }

        [Display(Name = "Yetkili Adı")]
        public string AuthorizedPerson { get; set; }

    }
}