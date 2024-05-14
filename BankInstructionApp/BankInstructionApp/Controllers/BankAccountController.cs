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
    public class BankAccountController : Controller
    {
        private Context db = new Context();
	
		public ActionResult BankAccounts()
        {
            var bankAccounts = db.BankAccounts.ToList();

            return View(bankAccounts);
        }


        public ActionResult NewBankAccount()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(bankAccount);
                db.SaveChanges();
                return RedirectToAction("BankAccounts", "BankAccount");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {

                    Console.WriteLine(error.ErrorMessage);
                }

                return View("NewBankAccount", bankAccount);
            }
        }
        public ActionResult UpdateBankAccount(int id)
        {
            var bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBankAccount(BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BankAccounts");
            }
            return View(bankAccount);
        }


        public ActionResult DeleteBankAccount(int id)
        {
            var bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            db.BankAccounts.Remove(bankAccount);
            db.SaveChanges();
            return RedirectToAction("BankAccounts");
        }


    }
}