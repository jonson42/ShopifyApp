using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Models
{
    public class ExcelExportModel
    {
        public string Image { get; set; }
        public string VariantTitle { get; set; }
        public string Price { get; set; }
        public string TotalPrice { get; set; }
        public string Quantity { get; set; }
        public string Order { get; set; }
        public string Phone { get; set; }
        public string SKU { get; set; }
        public string EMail { get; set; }
        public string ProductName { get; set; }
        public string OrderNotes { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Zip { get; set; }
        public string Address { get; set; }
        public string ShippingFullname { get; set; }
        public string ProvinceCode { get; set; }
        public string AddressFull { get; set; }
        public string CountryCode { get; set; }
        public string TransactionCard { get; set; }
        public string PayPalTransactionId { get; set; }
        public string Tracking { get; set; }
        public string TrackingUrl { get; set; }
        public string Carrer { get; set; }
        public string PaymentGateWay { get; set; }
    }
}
