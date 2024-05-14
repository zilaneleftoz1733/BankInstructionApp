using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankInstructionApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}