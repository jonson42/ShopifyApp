using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Models
{
    public class ShopMoney
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class PresentmentMoney
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class TotalLineItemsPriceSet
    {
        public ShopMoney shop_money { get; set; }
        public PresentmentMoney presentment_money { get; set; }
    }

    public class ShopMoney2
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class PresentmentMoney2
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class TotalDiscountsSet
    {
        public ShopMoney2 shop_money { get; set; }
        public PresentmentMoney2 presentment_money { get; set; }
    }

    public class ShopMoney3
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class PresentmentMoney3
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class TotalShippingPriceSet
    {
        public ShopMoney3 shop_money { get; set; }
        public PresentmentMoney3 presentment_money { get; set; }
    }

    public class ShopMoney4
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class PresentmentMoney4
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class SubtotalPriceSet
    {
        public ShopMoney4 shop_money { get; set; }
        public PresentmentMoney4 presentment_money { get; set; }
    }

    public class ShopMoney5
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class PresentmentMoney5
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class TotalPriceSet
    {
        public ShopMoney5 shop_money { get; set; }
        public PresentmentMoney5 presentment_money { get; set; }
    }

    public class ShopMoney6
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class PresentmentMoney6
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class TotalTaxSet
    {
        public ShopMoney6 shop_money { get; set; }
        public PresentmentMoney6 presentment_money { get; set; }
    }

    public class ShopMoney7
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class PresentmentMoney7
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class PriceSet
    {
        public ShopMoney7 shop_money { get; set; }
        public PresentmentMoney7 presentment_money { get; set; }
    }

    public class ShopMoney8
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class PresentmentMoney8
    {
        public string amount { get; set; }
        public string currency_code { get; set; }
    }

    public class TotalDiscountSet
    {
        public ShopMoney8 shop_money { get; set; }
        public PresentmentMoney8 presentment_money { get; set; }
    }

    public class LineItem
    {
        public string image { get; set; }
        public object id { get; set; }
        public object variant_id { get; set; }
        public string title { get; set; }
        public int quantity { get; set; }
        public string sku { get; set; }
        public string variant_title { get; set; }
        public string vendor { get; set; }
        public string fulfillment_service { get; set; }
        public object product_id { get; set; }
        public bool requires_shipping { get; set; }
        public bool taxable { get; set; }
        public bool gift_card { get; set; }
        public string name { get; set; }
        public object variant_inventory_management { get; set; }
        public List<object> properties { get; set; }
        public bool product_exists { get; set; }
        public int fulfillable_quantity { get; set; }
        public int grams { get; set; }
        public string price { get; set; }
        public string total_discount { get; set; }
        public object fulfillment_status { get; set; }
        public PriceSet price_set { get; set; }
        public TotalDiscountSet total_discount_set { get; set; }
        public List<object> discount_allocations { get; set; }
        public string admin_graphql_api_id { get; set; }
        public List<object> tax_lines { get; set; }
    }

    public class BillingAddress
    {
        public string first_name { get; set; }
        public string address1 { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string last_name { get; set; }
        public object address2 { get; set; }
        public object company { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string name { get; set; }
        public string country_code { get; set; }
        public string province_code { get; set; }
    }

    public class ShippingAddress
    {
        public string first_name { get; set; }
        public string address1 { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string last_name { get; set; }
        public object address2 { get; set; }
        public object company { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string name { get; set; }
        public string country_code { get; set; }
        public string province_code { get; set; }
    }

    public class DefaultAddress
    {
        public object id { get; set; }
        public object customer_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string company { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public string province_code { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public bool @default { get; set; }
    }

    public class Customer
    {
        public object id { get; set; }
        public string email { get; set; }
        public bool accepts_marketing { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int orders_count { get; set; }
        public string state { get; set; }
        public string total_spent { get; set; }
        public object last_order_id { get; set; }
        public object note { get; set; }
        public bool verified_email { get; set; }
        public object multipass_identifier { get; set; }
        public bool tax_exempt { get; set; }
        public object phone { get; set; }
        public string tags { get; set; }
        public string last_order_name { get; set; }
        public string currency { get; set; }
        public DateTime accepts_marketing_updated_at { get; set; }
        public object marketing_opt_in_level { get; set; }
        public List<object> tax_exemptions { get; set; }
        public string admin_graphql_api_id { get; set; }
        public DefaultAddress default_address { get; set; }
    }

    public class Order
    {
        
        public object id { get; set; }
        public string email { get; set; }
        public object closed_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int number { get; set; }
        public string note { get; set; }
        public string token { get; set; }
        public string gateway { get; set; }
        public bool test { get; set; }
        public string total_price { get; set; }
        public string subtotal_price { get; set; }
        public int total_weight { get; set; }
        public string total_tax { get; set; }
        public bool taxes_included { get; set; }
        public string currency { get; set; }
        public string financial_status { get; set; }
        public bool confirmed { get; set; }
        public string total_discounts { get; set; }
        public string total_line_items_price { get; set; }
        public object cart_token { get; set; }
        public bool buyer_accepts_marketing { get; set; }
        public string name { get; set; }
        public object referring_site { get; set; }
        public object landing_site { get; set; }
        public object cancelled_at { get; set; }
        public object cancel_reason { get; set; }
        public string total_price_usd { get; set; }
        public object checkout_token { get; set; }
        public object reference { get; set; }
        public object user_id { get; set; }
        public long? location_id { get; set; }
        public object source_identifier { get; set; }
        public object source_url { get; set; }
        public DateTime processed_at { get; set; }
        public object device_id { get; set; }
        public object phone { get; set; }
        public object customer_locale { get; set; }
        public int app_id { get; set; }
        public object browser_ip { get; set; }
        public object landing_site_ref { get; set; }
        public int order_number { get; set; }
        public List<object> discount_applications { get; set; }
        public List<object> discount_codes { get; set; }
        public List<object> note_attributes { get; set; }
        public List<string> payment_gateway_names { get; set; }
        public string processing_method { get; set; }
        public object checkout_id { get; set; }
        public string source_name { get; set; }
        public object fulfillment_status { get; set; }
        public List<object> tax_lines { get; set; }
        public string tags { get; set; }
        public string contact_email { get; set; }
        public string order_status_url { get; set; }
        public string presentment_currency { get; set; }
        public TotalLineItemsPriceSet total_line_items_price_set { get; set; }
        public TotalDiscountsSet total_discounts_set { get; set; }
        public TotalShippingPriceSet total_shipping_price_set { get; set; }
        public SubtotalPriceSet subtotal_price_set { get; set; }
        public TotalPriceSet total_price_set { get; set; }
        public TotalTaxSet total_tax_set { get; set; }
        public string total_tip_received { get; set; }
        public string admin_graphql_api_id { get; set; }
        public List<LineItem> line_items { get; set; }
        public List<object> shipping_lines { get; set; }
        public BillingAddress billing_address { get; set; }
        public ShippingAddress shipping_address { get; set; }
        public List<object> fulfillments { get; set; }
        public List<object> refunds { get; set; }
        public Customer customer { get; set; }
    }

    public class ShopifyModel
    {
        public List<Order> orders { get; set; }
    }
}
