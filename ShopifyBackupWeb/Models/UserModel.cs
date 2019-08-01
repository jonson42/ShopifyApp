using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Models
{
    public class UserModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Shop { get; set; }
        public string AppId { get; set; }
        public string AppPass { get; set; }
    }
}
