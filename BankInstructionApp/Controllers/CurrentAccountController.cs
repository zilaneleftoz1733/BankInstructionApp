
using BankInstructionApp.Filters;
using BankInstructionApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankInstructionApp.Controllers
{
	
	public class CurrentAccountController : Controller
    {

        private Context db = new Context();

		
		public ActionResult CustomerAccounts()
        {
            var customerAccounts = db.CustomerBankInfos.ToList();
            return View(customerAccounts);
        }

        public ActionResult NewCurrentAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerBankInfo customerBankInfo)
        {
            if (ModelState.IsValid)
            {
                db.CustomerBankInfos.Add(customerBankInfo);
                db.SaveChanges();
                return RedirectToAction("CustomerAccounts", "CurrentAccount");
            }
            else
            {
                
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    
                    Console.WriteLine(error.ErrorMessage);
                }


                return View("NewCurrentAccount", customerBankInfo);
            }
        }
        public ActionResult UpdateCustomerAccount(int id)
        {
            var customerAccount = db.CustomerBankInfos.Find(id);
            if (customerAccount == null)
            {
                return HttpNotFound();
            }
            return View(customerAccount);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCustomerAccount(CustomerBankInfo customerAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CustomerAccounts");
            }
            return View(customerAccount);
        }


        public ActionResult DeleteCustomerAccount(int id)
        {
            var customerAccount = db.CustomerBankInfos.Find(id);
            if (customerAccount == null)
            {
                return HttpNotFound();
            }
            db.CustomerBankInfos.Remove(customerAccount);
            db.SaveChanges();
            return RedirectToAction("CustomerAccounts");
        }

    }
}