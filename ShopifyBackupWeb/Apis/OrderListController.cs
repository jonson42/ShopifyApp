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
            List<string> listFullField = Utils.GetDataFromFile("ListFullField");
            List<string> listRefunds = Utils.GetDataFromFile("Refunds");
            Utils.SetProduct();
            var listOrder = new List<OrderModel>();
            //int Stt = 1;
            foreach(var itemSite in Utils.GetApp())
            {
                var data = Utils.GetDataFromLink("List_Order", "orders",itemSite);
                ShopifyModel shopifyModel = new ShopifyModel();
                shopifyModel = JsonConvert.DeserializeObject<ShopifyModel>(data);
                shopifyModel.orders.Reverse();
                if (shopifyModel!=null&&shopifyModel.orders != null)
                {
                    foreach (var item in shopifyModel.orders)
                    {
                        //show list 
                        #region
                        OrderModel orderModel = new OrderModel();
                        orderModel.shippingAddress = item.shipping_address;
                        //orderModel.Stt = Stt.ToString();
                        orderModel.Email = item.email != null ? item.email.ToString() : "";
                        orderModel.Id = item.id != null ? item.id.ToString() : "";
                        orderModel.Order = item.name != null ? item.name.ToString() : "";
                        orderModel.Date = item.created_at != null ? item.created_at.ToString() : "";
                        orderModel.Customer = item.customer != null && item.customer.first_name != null ? item.customer.first_name.ToString() : "";
                        orderModel.Payment = item.financial_status != null ? item.financial_status : "";
                        orderModel.Fulfillment = "UnFullField";
                      
                        foreach (var itemFullField in listFullField) {
                            if (orderModel.Order == itemFullField)
                            {
                                orderModel.Fulfillment = "FullField";
                                break;
                            }
                        }

                        foreach(var itemRefunds in listRefunds)
                        {
                            var check = itemRefunds.Split("~");
                            if (orderModel.Order == check[0])
                            {
                                orderModel.Payment = check[1];
                                break;
                            }
                        }
                        
                        orderModel.Total = item.total_price != null ? item.total_price : "";
                        #endregion
                        //Show list excel
                        #region
                        foreach (var itemSub in item.line_items)
                        {
                            try
                            {
                                var itemProduct = new ExcelExportModel();
                                itemProduct.Image = Utils.GetImageUrl(itemSub.title, itemSub.variant_id.ToString(), Utils.productModel);
                                itemProduct.Order = item.name;
                                itemProduct.VariantTitle = itemSub.variant_title != null ? itemSub.variant_title : "";
                                itemProduct.Price = itemSub.price != null ? itemSub.price : "";
                                itemProduct.TotalPrice = item.total_price != null ? item.total_price : "";
                                itemProduct.Quantity = itemSub.quantity != 0 ? itemSub.quantity.ToString() : "";
                                itemProduct.Phone = item.shipping_address != null ? item.shipping_address.phone.ToString() : "";
                                itemProduct.SKU = itemSub.sku != null ? itemSub.sku : "";
                                itemProduct.EMail = item.email != null ? item.email : "";
                                itemProduct.ProductName = itemSub.name != null ? itemSub.name : "";
                                itemProduct.OrderNotes = item.note != null ? item.note : "";
                                try
                                {
                                    itemProduct.City = item.shipping_address != null ? item.shipping_address.city.ToString() : "";
                                }
                                catch
                                {

                                }

                                itemProduct.Country = item.shipping_address != null ? item.shipping_address.country : "";
                                itemProduct.Province = item.shipping_address != null ? item.shipping_address.province : "";
                                itemProduct.Zip = item.shipping_address != null ? item.shipping_address.zip : "";
                                itemProduct.Address = item.shipping_address != null ? item.shipping_address.address1 : "";
                                itemProduct.ShippingFullname = item.shipping_address != null && item.shipping_address.last_name != null ? item.shipping_address.first_name + item.shipping_address.last_name : "";
                                itemProduct.ProvinceCode = item.shipping_address != null ? item.shipping_address.province_code : "";
                                itemProduct.AddressFull = item.shipping_address != null && item.shipping_address.address2 != null ? item.shipping_address.address1 + item.shipping_address.address2 : "";
                                itemProduct.CountryCode = item.shipping_address != null ? item.shipping_address.country_code : "";
                                orderModel.listProduct.Add(itemProduct);
                            }
                            catch
                            {

                            }
                           
                        }

                        #endregion
                        listOrder.Add(orderModel);
                       // Stt++;
                    }
                }
                
            }
            int Stt = listOrder.Count;
            //foreach(var item in listOrder)
            //{
            //    item.Stt = Stt.ToString();
            //    Stt--;
            //}
            var listTemp =new List<OrderModel>();
            for(int i = Stt; i > 0&&i!=0; i--)
            {
                listOrder[i-1].Stt = i.ToString();
                listTemp.Add(listOrder[i-1]);
            }
            return listTemp;
        }
        [HttpGet("getDetails")]
        public Order GetDetails(string idProduct)
        {
            var tracking = Utils.GetDataFromFile("Tracking");
            var result = new Order();
            foreach (var itemSite in Utils.GetApp())
            {
                var data = Utils.GetDataFromLink("List_Order", "orders",itemSite);
                
                ShopifyModel shopifyModel = new ShopifyModel();
                shopifyModel = JsonConvert.DeserializeObject<ShopifyModel>(data);
                if (shopifyModel != null && shopifyModel.orders != null)
                {
                    foreach (var item in shopifyModel.orders)
                    {
                        if (item.id.ToString() == idProduct)
                        {
                            foreach (var itemSub in item.line_items)
                            {
                                itemSub.image = Utils.GetImageUrl(itemSub.title, itemSub.variant_id.ToString(),Utils.productModel);
                                if (String.IsNullOrEmpty(itemSub.image))
                                {
                                    break;
                                }
                            }
                            result = item;
                            break;
                        }
                    }
                }
            }
            foreach(var item in tracking)
            {
                if (item.Split("|")[0].ToString()==result.name){
                    try
                    {
                        result.strackingNumber = item.Split("|")[2].ToString();
                        result.strackingUrl = item.Split("|")[1].ToString();
                        result.carrier = item.Split("|")[3].ToString();
                    }
                    catch  { }
                    
                }
            }
            return result;
        }
    }
}
