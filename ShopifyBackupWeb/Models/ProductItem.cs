using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Models
{
    public class ProductItem
    {
       public string Name { get; set; }
       public string Price { get; set; }
       public int Quantity { get; set; }
       public string Total { get; set; }
       public string Image { get; set; }
       public ProductItem()
       {
            Name = "";
            Total = "";
            Price = "";
            Quantity = 0;
            Image = "";
       }
    }
}
