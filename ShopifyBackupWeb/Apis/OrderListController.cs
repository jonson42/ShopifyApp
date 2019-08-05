using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopifyBackupWeb.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopifyBackupWeb.Apis
{
    [Route("api/shopify")]
    public class OrderListController : Controller
    {
        // GET: api/<controller>
        [HttpGet("getShopify")]
        public List<OrderModel> Get()
        {
            var listOrder = new List<OrderModel>();
            foreach(var itemSite in Utils.GetApp())
            {
                var data = Utils.GetDataFromLink("List_Order", "orders",itemSite);
                var product = Utils.GetDataFromLink("List_Product", "products", itemSite);
                ShopifyModel shopifyModel = new ShopifyModel();
                shopifyModel = JsonConvert.DeserializeObject<ShopifyModel>(data);
                var productModel = JsonConvert.DeserializeObject<ProductModel>(product);
                if (shopifyModel!=null&&shopifyModel.orders != null)
                {
                    foreach (var item in shopifyModel.orders)
                    {
                        //show list 
                        #region
                        OrderModel orderModel = new OrderModel();
                        orderModel.Id = item.id != null ? item.id.ToString() : "";
                        orderModel.Order = item.name != null ? item.name.ToString() : "";
                        orderModel.Date = item.created_at != null ? item.created_at.ToString() : "";
                        orderModel.Customer = item.customer != null && item.customer.first_name != null ? item.customer.first_name.ToString() : "";
                        orderModel.Payment = item.financial_status != null ? item.financial_status : "";
                        orderModel.Fulfillment = "UnFullField";
                        foreach (var itemFullField in Utils.GetOrderFullField()) {
                            if (orderModel.Order == itemFullField)
                            {
                                orderModel.Fulfillment = "FullField";
                                break;
                            }
                        }
                        
                        orderModel.Total = item.total_price != null ? item.total_price : "";
                        #endregion
                        //Show list excel
                        #region
                        foreach (var itemSub in item.line_items)
                        {
                            var itemProduct = new ExcelExportModel();

                            itemProduct.Order = item.name;
                            itemProduct.VariantTitle = itemSub.variant_title != null ? itemSub.variant_title : "";
                            itemProduct.Quantity = itemSub.quantity != 0 ? itemSub.quantity.ToString() : "";
                            itemProduct.Phone = item.phone != null ? item.phone.ToString() : "";
                            itemProduct.SKU = itemSub.sku != null ? itemSub.sku : "";
                            itemProduct.EMail = item.email != null ? item.email : "";
                            itemProduct.ProductName = itemSub.name != null ? itemSub.name : "";
                            itemProduct.OrderNotes = item.note != null ? item.note : "";
                            itemProduct.City = item.shipping_address != null ? item.shipping_address.ToString() : "";
                            itemProduct.Country = item.shipping_address != null ? item.shipping_address.country : "";
                            itemProduct.Province = item.shipping_address != null ? item.shipping_address.province : "";
                            itemProduct.Zip = item.shipping_address != null ? item.shipping_address.zip : "";
                            itemProduct.Address = item.shipping_address != null ? item.shipping_address.address1 : "";
                            itemProduct.ShippingFullname = item.shipping_address != null && item.shipping_address.last_name != null ? item.shipping_address.first_name + item.shipping_address.last_name : "";
                            itemProduct.ProvinceCode = item.shipping_address != null ? item.shipping_address.province_code : "";
                            itemProduct.AddressFull = item.shipping_address != null && item.shipping_address.address2 != null ? item.shipping_address.address1 + item.shipping_address.address2 : "";
                            itemProduct.CountryCode = item.shipping_address != null ? item.shipping_address.country : "";
                            orderModel.listProduct.Add(itemProduct);
                        }

                        #endregion
                        listOrder.Add(orderModel);
                    }
                }
                
            }
            
            return listOrder;
        }
        [HttpGet("getDetails")]
        public Order GetDetails(string idProduct)
        {
            var result = new Order();
            foreach (var itemSite in Utils.GetApp())
            {
                var data = Utils.GetDataFromLink("List_Order", "orders",itemSite);
                ShopifyModel shopifyModel = new ShopifyModel();
                var product = Utils.GetDataFromLink("List_Product", "products", itemSite);
                shopifyModel = JsonConvert.DeserializeObject<ShopifyModel>(data);
                
                var productModel = JsonConvert.DeserializeObject<ProductModel>(product);
                if (shopifyModel != null && shopifyModel.orders != null)
                {
                    foreach (var item in shopifyModel.orders)
                    {
                        if (item.id.ToString() == idProduct)
                        {
                            foreach (var itemSub in item.line_items)
                            {
                                foreach (var itemProduct in productModel.products)
                                {
                                    foreach (var itemImage in itemProduct.images)
                                    {
                                        foreach (var itemVari in itemImage.variant_ids)
                                        {
                                            if (itemVari.ToString() == itemSub.variant_id.ToString())
                                            {
                                                itemSub.image = itemImage.src;
                                                break;
                                            }
                                        }
                                    }
                                    if (itemSub.image == null)
                                    {
                                        if (itemProduct.title == itemSub.title)
                                        {
                                            itemSub.image = itemProduct.images[0].src;
                                        }
                                    }

                                }

                            }
                            result = item;
                            break;
                        }
                    }
                }
            }
            return result;
        }
    }
}
