using BankInstructionApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankInstructionApp.Controllers
{
    public class InstructionController : Controller
    {

		private Context db = new Context();

		public ActionResult Instructions()
		{

			var instructions = db.InstructionViewModels.ToList();

			// Banka hesapları 
			var bankAccountsList = db.BankAccounts
				.ToList() 
				.Select(b => new SelectListItem
				{
					Value = b.BankAccountId.ToString(),
					Text = "Banka Adı: " + b.BankName + "\n" +
						   "Şube Adı: " + b.BranchName + "\n" +
						   "IBAN: " + b.IBAN + "\n" +
						   "Para Birimi: " + b.Currency + "\n" +
						   "Açıklama: " + b.Description
				})
				.ToList();

			// Müşteri hesapları 
			foreach (var instruction in instructions)
			{
				int[] ids = instruction.CustomerBankInfoIds?.Split(',').Select(int.Parse).ToArray();
				if (ids != null)
				{
					instruction.YourCustomerBankInfoList = new List<SelectListItem>();
					foreach (var id in ids)
					{
						var customerBankInfo = db.CustomerBankInfos.FirstOrDefault(c => c.CustomerBankInfoId == id);
						if (customerBankInfo != null)
						{
							SelectListItem item = new SelectListItem
							{
								Value = customerBankInfo.CustomerBankInfoId.ToString(),
								Text = "Kullanıcı Kodu: " + customerBankInfo.CustomerCode + "\n" +
									   "Kullanıcı Adı: " + customerBankInfo.CustomerName + "\n" +
									   "Banka Adı: " + customerBankInfo.BankName + "\n" +
									   "Şube Adı: " + customerBankInfo.BranchName + "\n" +
									   "IBAN: " + customerBankInfo.IBAN + "\n" +
									   "Vergi Numarası: " + customerBankInfo.TaxNumber + "\n" +
									   "Para Birimi: " + customerBankInfo.Currency
							};
							instruction.YourCustomerBankInfoList.Add(item);
						}
					}
				}
			}

			// İşlem türleri 
			var operationTypesList = db.OperationTypes
				.ToList()
				.Select(o => new SelectListItem
				{
					Value = o.opreationID.ToString(),
					Text = "İşlem Türü: " + o.BankOperationTpye + "\n" +
						   "Açıklama: " + o.Description
				})
				.ToList();

			foreach (var instruction in instructions)
			{
				instruction.YourBankAccountList = bankAccountsList;
				instruction.YourOperationTypeList = operationTypesList;
			}

			return View(instructions);
		}

		public ActionResult NewInstructions()
		{
			var bankAccountsList = db.BankAccounts.Select(b => new SelectListItem { Value = b.BankAccountId.ToString(), Text = b.BankName }).ToList();
			var customerAccountsList = db.CustomerBankInfos.Select(c => new SelectListItem { Value = c.CustomerBankInfoId.ToString(), Text = c.CustomerName }).ToList();
			var operationTypesList = db.OperationTypes.Select(o => new SelectListItem { Value = o.opreationID.ToString(), Text = o.BankOperationTpye }).ToList();

			ViewBag.BankAccounts = new SelectList(bankAccountsList, "Value", "Text");
			ViewBag.CustomerAccounts = new MultiSelectList(customerAccountsList, "Value", "Text");
			ViewBag.SelectedCustomerBankInfoIds = new MultiSelectList(customerAccountsList, "Value", "Text"); 

			ViewBag.OperationTypes = new SelectList(operationTypesList, "Value", "Text");

			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(InstructionViewModel instructionViewModel, int[] selectedCustomerBankInfoIds)
		{
			if (selectedCustomerBankInfoIds != null)
			{
				instructionViewModel.CustomerBankInfoIds = string.Join(",", selectedCustomerBankInfoIds);

				db.InstructionViewModels.Add(instructionViewModel);
				db.SaveChanges();
				return RedirectToAction("Instructions", "Instruction");
			}
			else
			{
				var bankAccountsList = db.BankAccounts.Select(b => new SelectListItem { Value = b.BankAccountId.ToString(), Text = b.Description }).ToList();
				ViewBag.BankAccounts = new SelectList(bankAccountsList, "Value", "Text");

				return View("NewInstructions", instructionViewModel);
			}
		}

		public ActionResult UpdateInstruction(int id)
		{
			var instruction = db.InstructionViewModels.Find(id);
			if (instruction == null)
			{
				return HttpNotFound();
			}

			var bankAccountsList = db.BankAccounts.Select(b => new SelectListItem { Value = b.BankAccountId.ToString(), Text = "Banka Adı: " + b.BankName + "Banka Adı\n" + "Şube Adı: " + b.BranchName + "\n" + "IBAN: " + b.IBAN + "\n" + "Para Birimi: " + b.Currency + "\n" + "Açıklama: " + b.Description }).ToList();
			var customerAccountsList = db.CustomerBankInfos.Select(c => new SelectListItem { Value = c.CustomerBankInfoId.ToString(), Text = ": " + c.CustomerCode + "\n" + "Kullanıcı Adı: " + c.CustomerName + "\n" + "Banka Adı: " + c.BankName + "\n" + "Şube Adı: " + c.BranchName + "Şube Adı\n" + "IBAN: " + c.IBAN + "\n" + "Vergi Numarası: " + c.TaxNumber + "\n" + "Para Birimi : " + c.Currency }).ToList();
			var operationTypesList = db.OperationTypes.Select(o => new SelectListItem { Value = o.opreationID.ToString(), Text = "İşlem Türü: " + o.BankOperationTpye + "\n" + "Açıklama: " + o.Description }).ToList();

			var selectedCustomerBankInfoIds = instruction.CustomerBankInfoIds?.Split(',').ToList();

			var selectedIds = selectedCustomerBankInfoIds?.Select(int.Parse).ToList();

			// Eşleşen ID'leri bulmak için customerAccountsList'i filtreleme
			var matchingIndexes = customerAccountsList
				.Select((item, index) => new { Item = item, Index = index }) // Öğelerin index değerlerini aldık
				.Where(pair => selectedIds != null && selectedIds.Contains(int.Parse(pair.Item.Value))) // Seçilen ID'lerle eşleşenleri filtreledik
				.Select(pair => pair.Index) // Eşleşen öğelerin index değerlerini alındı
				.ToList()
				.Select(x => x.ToString())
				.ToArray();

			instruction.YourBankAccountList = bankAccountsList;
			instruction.YourCustomerBankInfoList = customerAccountsList;
			instruction.YourOperationTypeList = operationTypesList;
			ViewBag.SelectedCustomerBankInfoIds = new MultiSelectList(customerAccountsList, "Value", "Text", matchingIndexes);

			return View(instruction);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UpdateInstruction(InstructionViewModel instruction, int[] selectedCustomerBankInfoIds)
		{
			if (instruction != null)
			{
				instruction.CustomerBankInfoIds = string.Join(",", selectedCustomerBankInfoIds);
				db.Entry(instruction).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Instructions");
			}
			return View(instruction);
		}

		public ActionResult DeleteInstruction(int id)
		{
			var instruction = db.InstructionViewModels.Find(id);
			if (instruction == null)
			{
				return HttpNotFound();
			}
			db.InstructionViewModels.Remove(instruction);
			db.SaveChanges();
			return RedirectToAction("Instructions");
		}

	}

}
