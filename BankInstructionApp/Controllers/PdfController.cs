using BankInstructionApp.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BankInstructionApp.Controllers
{
    public class PdfController : Controller
    {
        private Context db = new Context();


        public FileResult GenerateInstructionsPdf()
        {
            var instructions = db.InstructionViewModels.ToList();

            // Talimatlar içindeki her bir veri için gerekli bilgileri doldurma
            foreach (var instruction in instructions)
            {
                
                instruction.YourBankAccountList = db.BankAccounts
                    .Select(b => new SelectListItem
                    {
                        Value = b.BankAccountId.ToString(),
                        Text = "Banka Adı: " + b.BankName + "\n" +
                               "Şube Adı: " + b.BranchName + "\n" +
                               "IBAN: " + b.IBAN + "\n" +
                               "Para Birimi: " + b.Currency + "\n" +
                               "Açıklama: " + b.Description
                    }).ToList();

               
                int[] ids = instruction.CustomerBankInfoIds?.Split(',').Select(int.Parse).ToArray();
                if (ids != null)
                {
                    instruction.YourCustomerBankInfoList = db.CustomerBankInfos
                        .Where(c => ids.Contains(c.CustomerBankInfoId))
                        .Select(c => new SelectListItem
                        {
                            Value = c.CustomerBankInfoId.ToString(),
                            Text = "Kullanıcı Kodu: " + c.CustomerCode + "\n" +
                                   "Kullanıcı Adı: " + c.CustomerName + "\n" +
                                   "Banka Adı: " + c.BankName + "\n" +
                                   "Şube Adı: " + c.BranchName + "\n" +
                                   "IBAN: " + c.IBAN + "\n" +
                                   "Vergi Numarası: " + c.TaxNumber + "\n" +
                                   "Para Birimi: " + c.Currency
                        }).ToList();
                }

                instruction.YourOperationTypeList = db.OperationTypes
                    .Select(o => new SelectListItem
                    {
                        Value = o.opreationID.ToString(),
                        Text = "İşlem Türü: " + o.BankOperationTpye + "\n" +
                               "Açıklama: " + o.Description

                    }).ToList();
            }

            // PDF belgesi oluşturma
            Document document = new Document();
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

           
            BaseFont baseFont = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font fontContentNormal = new Font(baseFont, 12f, Font.NORMAL);
            Font fontBold = new Font(baseFont, 12f, Font.BOLD);

            
            Font fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18f, BaseColor.BLACK);
            Paragraph title = new Paragraph("TALIMATLAR", fontTitle);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);
            document.Add(new Paragraph("\n"));

            // Tablo oluşturma
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100; 
            table.SpacingAfter = 20f;

            
            string[] columnHeaders = { "Talimat ID", "Banka Hesabı", "Cari Hesaplar", "İşlem Türü" };
            foreach (var header in columnHeaders)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(header, fontBold));
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(headerCell);
            }

            
            foreach (var instruction in instructions)
            {
               
                PdfPCell idCell = new PdfPCell(new Phrase(instruction.Id.ToString(), fontBold));
                idCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(idCell);

                
                var bankAccountListItem = instruction.YourBankAccountList?.FirstOrDefault(b => b.Value == instruction.BankAccountId.ToString());
                string bankAccountText = bankAccountListItem != null ? bankAccountListItem.Text : "N/A";
                PdfPCell bankAccountCell = new PdfPCell(new Phrase(bankAccountText, fontContentNormal));
                table.AddCell(bankAccountCell);

                
                string customerAccountsText = string.Join("\n", instruction.YourCustomerBankInfoList.Select(item => item.Text)); 
                PdfPCell customerAccountsCell = new PdfPCell(new Phrase(customerAccountsText, fontContentNormal)); 
                table.AddCell(customerAccountsCell);

                
                var operationTypeListItem = instruction.YourOperationTypeList?.FirstOrDefault(o => o.Value == instruction.operationTypeID.ToString());
                string operationTypeText = operationTypeListItem != null ? operationTypeListItem.Text : "N/A";
                PdfPCell operationTypeCell = new PdfPCell(new Phrase(operationTypeText, fontContentNormal));
                table.AddCell(operationTypeCell);
            }

            // Tabloyu belgeye ekle
            document.Add(table);
            document.Close();

            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            return File(bytes, "application/pdf", "talimatlar.pdf");
        }
    }
}