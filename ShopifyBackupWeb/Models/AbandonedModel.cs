using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Models
{
        public class Checkout
        {
            public object id { get; set; }
            public string token { get; set; }
            public string cart_token { get; set; }
            public string email { get; set; }
            public string gateway { get; set; }
            public bool buyer_accepts_marketing { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string landing_site { get; set; }
            public string note { get; set; }
            public List<object> note_attributes { get; set; }
            public string referring_site { get; set; }
            public List<object> shipping_lines { get; set; }
            public bool taxes_included { get; set; }
            public int total_weight { get; set; }
            public string currency { get; set; }
            public DateTime? completed_at { get; set; }
            public object closed_at { get; set; }
            public object user_id { get; set; }
            public object location_id { get; set; }
            public object source_identifier { get; set; }
            public object source_url { get; set; }
            public object device_id { get; set; }
            public object phone { get; set; }
            public string customer_locale { get; set; }
            public List<object> line_items { get; set; }
            public string name { get; set; }
            public object source { get; set; }
            public string abandoned_checkout_url { get; set; }
            public List<object> discount_codes { get; set; }
            public List<object> tax_lines { get; set; }
            public string source_name { get; set; }
            public string presentment_currency { get; set; }
            public string total_discounts { get; set; }
            public string total_line_items_price { get; set; }
            public string total_price { get; set; }
            public string total_tax { get; set; }
            public string subtotal_price { get; set; }
            public BillingAddress billing_address { get; set; }
            public ShippingAddress shipping_address { get; set; }
            public Customer customer { get; set; }
        }

        public class AbandonedModel
    {
            public List<Checkout> checkouts { get; set; }
        }
}
