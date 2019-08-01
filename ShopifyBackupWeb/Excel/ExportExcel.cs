using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ShopifyBackupWeb.Apis;
using ShopifyBackupWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShopifyBackupWeb.Excel
{
    public class ExportExcel: ControllerBase
    {
        public IActionResult ExportDataToExcel(List<ExcelExportModel> excelItem,string fileName)
        {
            //Dump
            #region
            //ExcelExportModel item = new ExcelExportModel();
            //foreach(var data in excelItem)
            //{
            //    item.Image = data.Image;
            //    item.VariantTitle = data.VariantTitle;
            //    item.Quantity = data.Quantity;
            //    item.Order = data.Order;
            //    item.Phone = data.Phone;
            //    item.SKU = data.SKU;
            //    item.EMail = data.EMail;
            //    item.ProductName = data.ProductName;
            //    item.OrderNotes = data.OrderNotes;
            //    item.City = data.City;
            //    item.Country = data.Country;
            //    item.Province = data.Province;
            //    item.Zip = data.Zip;
            //    item.Address = data.Address;
            //    item.ShippingFullname = data.ShippingFullname;
            //    item.ProvinceCode = data.ProvinceCode;
            //    item.AddressFull = data.AddressFull;
            //    item.CountryCode = data.CountryCode;
            //    item.TransactionCard = data.TransactionCard;
            //    item.PayPalTransactionId = data.PayPalTransactionId;
            //    excelItem.Add(item);
            //}
            #endregion
            try
            {
                //read the Excel file as byte array
                string fileSaveAs = fileName.Replace("Export.xlsx", "Out\\ExportOut.xlsx");
                FileInfo fileOut = new FileInfo(fileSaveAs);
                if (fileOut.Exists)
                {
                    fileOut.Delete();
                }
                fileOut.Delete();
                byte[] bin = System.IO.File.ReadAllBytes(fileName);
                FileInfo file = new FileInfo(fileName);
                //create a new Excel package in a memorystream
                using (MemoryStream stream = new MemoryStream(bin))
                using (ExcelPackage excelPackage = new ExcelPackage(file))
                {
                    //create an instance of the the first sheet in the loaded file
                    foreach (ExcelWorksheet worksheet in excelPackage.Workbook.Worksheets)
                    {
                        int i = 2;
                        if (worksheet.Name == "Data")
                        {
                            foreach (var itemSub in excelItem)
                            {
                                worksheet.Cells[i, 1].Value = itemSub.Image;
                                worksheet.Cells[i, 2].Value = itemSub.VariantTitle;
                                worksheet.Cells[i, 3].Value = itemSub.Quantity;
                                worksheet.Cells[i, 4].Value = itemSub.Order;
                                worksheet.Cells[i, 5].Value = itemSub.Phone;
                                worksheet.Cells[i, 6].Value = itemSub.SKU;
                                worksheet.Cells[i, 7].Value = itemSub.EMail;
                                worksheet.Cells[i, 8].Value = itemSub.ProductName;
                                worksheet.Cells[i, 9].Value = itemSub.OrderNotes;
                                worksheet.Cells[i, 10].Value = itemSub.City;
                                worksheet.Cells[i, 11].Value = itemSub.Country;
                                worksheet.Cells[i, 12].Value = itemSub.Province;
                                worksheet.Cells[i, 13].Value = itemSub.Zip;
                                worksheet.Cells[i, 14].Value = itemSub.Address;
                                worksheet.Cells[i, 15].Value = itemSub.ShippingFullname;
                                worksheet.Cells[i, 16].Value = itemSub.ProvinceCode;
                                worksheet.Cells[i, 17].Value = itemSub.AddressFull;
                                worksheet.Cells[i, 18].Value = itemSub.CountryCode;
                                worksheet.Cells[i, 19].Value = itemSub.TransactionCard;
                                worksheet.Cells[i, 20].Value = itemSub.PayPalTransactionId;
                                i++;
                            }

                        }
                    }
                    //save the changes
                    
                    FileInfo fileInfo1 = new FileInfo(fileSaveAs);
                    excelPackage.SaveAs(fileInfo1);
                }
            }
            catch (Exception ex)
            {
                var a = 0;
            }
            FileInfo fileInfo = new FileInfo(fileName);
            //Create file 
            IActionResult mActionResult = new EmptyResult();
            #region
            if (fileInfo.Exists)
            {

                mActionResult = new FileContentResult(System.IO.File.ReadAllBytes(fileName), "application/octet-stream")
                {
                    FileDownloadName = "Export.xlsx"
                };
            }
            //byte[] bin1 = System.IO.File.ReadAllBytes(fileName);
            return mActionResult;
            #endregion
            // return new FilePathResult(fileName, "application/zip"); ;
        }
    }
}
