using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Models
{
    public class OrderModel
    {
        public string Stt { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Order { get; set; }
        public string Date { get; set; }
        public string Customer { get; set; }
        public string Payment { get; set; }
        public string Fulfillment { get; set; }
        public string Total { get; set; }
        public ShippingAddress shippingAddress { get; set; }
        //File Export Excel
        public List<ExcelExportModel> listProduct{get;set;}

        public OrderModel()
        {
            Order = "";
            Date = "";
            Customer = "";
            Payment = "";
            Fulfillment = "";
            Total = "";
            listProduct = new List<ExcelExportModel>();

        }
    }
}
