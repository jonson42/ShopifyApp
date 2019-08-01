using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Models
{
    public class AbandonedListModel
    {
        public string Checkout { get; set; }
        public string Date { get; set; }
        public string PlacedBy { get; set; }
        public string EmailStatus { get; set; }
        public string RecoveryStatus { get; set; }
        public string Total { get; set; }
    }
}
