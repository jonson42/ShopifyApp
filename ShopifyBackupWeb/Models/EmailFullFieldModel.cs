﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Models
{
    public class EmailFullFieldModel
    {
        public Order Order { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Carrier { get; set; }
        public List<LineItem> ListItem { get; set; }
        public string TrackingUrl { get; set; }
    }
}
