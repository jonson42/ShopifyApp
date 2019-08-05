using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Models
{
    public class EmailRefundsModel
    {
        public string MoneyRefunds { get; set; }
        public string Email { get; set; }
        public Order Order { get; set; }
    }
}
