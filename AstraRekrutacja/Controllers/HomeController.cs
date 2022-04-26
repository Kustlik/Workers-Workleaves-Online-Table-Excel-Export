using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AstraRekrutacja.JsonObjects;
using AstraRekrutacja.Controllers;
using ClosedXML.Excel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace AstraRekrutacja.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Astrafox";

            return View();
        }

        public ActionResult WorkleavesTable()
        {
            ViewBag.Message = "Your database table.";

            return View();
        }

        // Button action to export data
        public async Task WriteDataToExcel()
        {
            string sheetName = "Workleaves";

            try
            {
                List<WorkleavesJson> data = await WorkersController.GetWorkleavesData();

                using (IXLWorkbook workbook = new XLWorkbook())
                {
                    //Create Excel file
                    workbook.AddWorksheet(sheetName).FirstCell().InsertTable(data, false);

                    //Create Stream for download
                    Stream fileStream = new MemoryStream();
                    workbook.SaveAs(fileStream);
                    fileStream.Position = 0;

                    string myName = Server.UrlEncode(sheetName + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
                    MemoryStream stream = (MemoryStream)fileStream;

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment; filename=" + myName);
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
