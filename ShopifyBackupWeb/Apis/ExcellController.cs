using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ShopifyBackupWeb.Excel;
using ShopifyBackupWeb.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopifyBackupWeb.Apis
{
    [Route("api/shopify")]
    public class ExcellController : Controller
    {
        // GET: api/<controller>
        [HttpPost("exportExcel")]
        public string ExportExcel([FromBody]List<Models.ExcelExportModel> inparam)
        {
            ExportExcel excel = new ExportExcel();
            var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Data_Init\\Export.xlsx");
             excel.ExportDataToExcel(inparam, filePath);
            return "";
        }

        [HttpPost("importExcel")]
        public List<ExcelExportModel> ImportExcel()
        {
            List<ExcelExportModel> list = new List<ExcelExportModel>();
            IFormFile postedFile = Request.Form.Files[0];
            var tempPath = "";
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            tempPath = path + "\\Data_Init\\Import\\Import.xlsx";
            using (FileStream fileStream = new FileStream(tempPath, FileMode.Create))
            {
                postedFile.CopyToAsync(fileStream).Wait();
            }
            byte[] bin = System.IO.File.ReadAllBytes(tempPath);
            FileInfo file = new FileInfo(tempPath);
            //create a new Excel package in a memorystream
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                //create an instance of the the first sheet in the loaded file
                foreach (ExcelWorksheet worksheet in excelPackage.Workbook.Worksheets)
                {
                    if (worksheet.Name == "Data")
                    {
                        for (var i=2;i< worksheet.Dimension.End.Row;i++)
                        {
                            ExcelExportModel excelItem = new ExcelExportModel();
                            if (worksheet.Cells[i, 1].Value != null)
                            {
                                excelItem.Image = worksheet.Cells[i, 1].Value.ToString();
                            }

                            if (worksheet.Cells[i, 2].Value != null)
                            {
                                excelItem.VariantTitle = worksheet.Cells[i, 2].Value.ToString();
                            }
                            if (worksheet.Cells[i, 3].Value != null)
                            {
                                excelItem.Price = worksheet.Cells[i, 3].Value.ToString();
                            }
                            if (worksheet.Cells[i, 4].Value != null)
                            {
                                excelItem.TotalPrice = worksheet.Cells[i, 4].Value.ToString();
                            }
                            if (worksheet.Cells[i, 5].Value != null)
                            {
                                excelItem.Quantity = worksheet.Cells[i, 5].Value.ToString();
                            }
                            if (worksheet.Cells[i, 6].Value != null)
                            {
                                excelItem.Order = worksheet.Cells[i, 6].Value.ToString();
                            }
                            if (worksheet.Cells[i, 7].Value != null)
                            {
                                excelItem.Phone = worksheet.Cells[i, 7].Value.ToString();
                            }
                            if (worksheet.Cells[i, 8].Value != null)
                            {
                                excelItem.SKU = worksheet.Cells[i, 8].Value.ToString();
                            }
                            if (worksheet.Cells[i, 9].Value != null)
                            {
                                excelItem.EMail = worksheet.Cells[i, 9].Value.ToString();
                            }
                            if (worksheet.Cells[i, 10].Value != null)
                            {
                                excelItem.ProductName = worksheet.Cells[i,10].Value.ToString();
                            }
                            if (worksheet.Cells[i, 11].Value != null)
                            {
                                excelItem.OrderNotes = worksheet.Cells[i, 11].Value.ToString();
                            }
                            if (worksheet.Cells[i, 12].Value != null)
                            {
                                excelItem.City = worksheet.Cells[i, 12].Value.ToString();
                            }
                            if (worksheet.Cells[i, 13].Value != null)
                            {
                                excelItem.Country = worksheet.Cells[i, 13].Value.ToString();
                            }
                            if (worksheet.Cells[i, 14].Value != null)
                            {
                                excelItem.Province = worksheet.Cells[i, 14].Value.ToString();
                            }
                            if (worksheet.Cells[i, 15].Value != null)
                            {
                                excelItem.Zip = worksheet.Cells[i, 15].Value.ToString();
                            }
                            if (worksheet.Cells[i, 16].Value != null)
                            {
                                excelItem.Address = worksheet.Cells[i, 16].Value.ToString();
                            }
                            if (worksheet.Cells[i, 17].Value != null)
                            {
                                excelItem.ShippingFullname = worksheet.Cells[i, 17].Value.ToString();
                            }
                            if (worksheet.Cells[i, 18].Value != null)
                            {
                                excelItem.ProvinceCode = worksheet.Cells[i, 18].Value.ToString();
                            }
                            if (worksheet.Cells[i, 19].Value != null)
                            {
                                excelItem.AddressFull = worksheet.Cells[i, 19].Value.ToString();
                            }
                            if (worksheet.Cells[i, 20].Value != null)
                            {
                                excelItem.CountryCode = worksheet.Cells[i, 20].Value.ToString();
                            }
                            if (worksheet.Cells[i, 21].Value != null)
                            {
                                excelItem.TransactionCard = worksheet.Cells[i, 21].Value.ToString();
                            }
                            if (worksheet.Cells[i, 20].Value != null)
                            {
                                excelItem.PayPalTransactionId = worksheet.Cells[i, 20].Value.ToString();
                            }
                            if (worksheet.Cells[i, 22].Value != null)
                            {
                                excelItem.Tracking = worksheet.Cells[i, 22].Value.ToString();
                            }
                            if (worksheet.Cells[i, 23].Value != null)
                            {
                                excelItem.TrackingUrl = worksheet.Cells[i, 23].Value.ToString();
                            }
                            if (worksheet.Cells[i, 24].Value != null)
                            {
                                excelItem.Carrer = worksheet.Cells[i, 24].Value.ToString();
                            }
                            list.Add(excelItem);
                        }

                    }
                }
            }
            return list;
        }
        
    }
}
