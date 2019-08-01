using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Models
{
    public class ProductDetails
    {
        public string TotalPrice { get; set; }
        public string TotalTax { get; set; }
        public List<ProductItem> ProductItem { get; set; }
        public ProductDetails()
        {
            TotalPrice = "";
            TotalTax = "";
            ProductItem = new List<ProductItem>();
        }
    }
}
