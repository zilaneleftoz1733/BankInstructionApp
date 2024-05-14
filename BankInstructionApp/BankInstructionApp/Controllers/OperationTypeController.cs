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
	
	public class OperationTypeController : Controller
    {
		private Context db = new Context();

	
		public ActionResult OperationType()
		{
			var operationTypes = db.OperationTypes.ToList();
			return View(operationTypes);
		}

		
		public ActionResult NewOperationType()
		{
			return View();
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(OperationType operationType)
		{
			if (ModelState.IsValid)
			{
				db.OperationTypes.Add(operationType);
				db.SaveChanges();
				return RedirectToAction("OperationType", "OperationType");
			}
			else
			{

				var errors = ModelState.Values.SelectMany(v => v.Errors);
				foreach (var error in errors)
				{

					Console.WriteLine(error.ErrorMessage);
				}


				return View("NewOperationType", operationType);
			}
		}
        public ActionResult UpdateOperationType(int id)
        {
            var operationType = db.OperationTypes.Find(id);
            if (operationType == null)
            {
                return HttpNotFound();
            }
            return View(operationType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateOperationType(OperationType operationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operationType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OperationType");
            }
            return View(operationType);
        }

        public ActionResult DeleteOperationType(int id)
        {
            var operationType = db.OperationTypes.Find(id);
            if (operationType == null)
            {
                return HttpNotFound();
            }
            db.OperationTypes.Remove(operationType);
            db.SaveChanges();
            return RedirectToAction("OperationType");
        }
    }
}