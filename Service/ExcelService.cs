using AutoConsent.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace AutoConsent
{
    public class ExcelService : IFileService
    {
        private readonly IRepository _repository;

        public ExcelService(IRepository repository)
        {
            _repository = repository;
        }

        public bool PrintFile()
        {
            bool isSuccess;
            var app = new Application();
            var workbook = app.Workbooks.Add();
            try
            {

                var worksheet = workbook.Worksheets.Add();
                PrintTitles(worksheet);
                int rowIndex = 2;
                var recordEnum = (_repository.Items[Constants.Keys.Records.ToString()] as List<MailRecord>).GetEnumerator();
                while (recordEnum.MoveNext())
                {
                    PrintRecord(rowIndex, worksheet, recordEnum.Current);
                    rowIndex++;
                }
                isSuccess = true;
            }
            catch
            {
                isSuccess = false;
            }
            finally
            {
                string fileFullName = Path.Combine(_repository.Items[Constants.Keys.Directory.ToString()].ToString(), $"Report_{DateTime.Now.ToString(@"ddMMyyyy_hhmm")}");
                workbook.SaveAs(fileFullName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
            false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close();
                app.Quit();
                
            }
            return isSuccess;
        }
        private void PrintTitles(Worksheet worksheet)
        {
            worksheet.Cells[1, 1] = "#";
            worksheet.Cells[1, 2] = "FullName";
            worksheet.Cells[1, 3] = "Telephone";
            worksheet.Cells[1, 4] = "Email";
            worksheet.Cells[1, 5] = "Confirmation date";
            worksheet.Cells[1, 6] = "Comment";
            worksheet.Cells[1, 7] = "Consent1";
            worksheet.Cells[1, 8] = "Consent2";
            worksheet.Cells[1, 9] = "Consent3";
            worksheet.Cells[1, 10] = "Consent4";
            worksheet.Cells[1, 11] = "...";
        }
        private void PrintRecord(int rowIndex, Worksheet worksheet, MailRecord record)
        {
            worksheet.Cells[rowIndex, 1] = record.Id;
            worksheet.Cells[rowIndex, 2] = record.FullName;
            worksheet.Cells[rowIndex, 3] = record.Telephone;
            worksheet.Cells[rowIndex, 4] = record.Email;
            worksheet.Cells[rowIndex, 5] = record.ConfirmationDate;
            worksheet.Cells[rowIndex, 6] = record.Comment ?? string.Empty;
            int colIndex = 7;
            var consentEnum = record.Consents.GetEnumerator();
            while(consentEnum.MoveNext())
            {
                worksheet.Cells[rowIndex, colIndex] = consentEnum.Current;
                colIndex++;
            }           
        }
    }
}
