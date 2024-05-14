using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankInstructionApp.Filters;
using BankInstructionApp.Models;

namespace BankInstructionApp.Controllers
{

    public class HomeController : Controller
    {
        private Context db = new Context();
		
		

		public ActionResult CustomerAccounts()
        {

            var customerAccounts = db.CustomerBankInfos.ToList();


            return View(customerAccounts);
        }

		public ActionResult BankAccounts()
        {
            var bankAccounts = db.BankAccounts.ToList();


            return View(bankAccounts);
        }

        public ActionResult Instructions()
        {
            var instructions = db.InstructionViewModels.ToList();
            return View(instructions);
        }


        public ActionResult BankInstructionApp()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult OperationType()
        {

            var operationType = db.OperationTypes.ToList();


            return View(operationType);
        }
    }
}