using Newtonsoft.Json;
using ShopifyBackupWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyBackupWeb
{
    public static class Utils
    {
        public static ProductModel productModel { get; set; }
        public static EmailModel emailModel { get; set; }
        public static DNSModel dnsModel { get; set; }
        public static EmailContactsModel emailContacts { get; set; }
        public static ShopNameModel shopName { get; set; }
        public static void SetEmailContacts()
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\EmailContacts.json");
            var data = "";
            if (System.IO.File.Exists(path))
            {
                data = System.IO.File.ReadAllText(path);
            }
            emailContacts = JsonConvert.DeserializeObject<EmailContactsModel>(data);
        }
        public static void SetDnsModel()
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\DNS.json");
            var data = "";
            if (System.IO.File.Exists(path))
            {
                data = System.IO.File.ReadAllText(path);
            }
            dnsModel = JsonConvert.DeserializeObject<DNSModel>(data);
        }

        public static void updateShopNameDefault()
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\ShopName.json");
            var data = "";
            if (System.IO.File.Exists(path))
            {
                data = System.IO.File.ReadAllText(path);
            }
            shopName = JsonConvert.DeserializeObject<ShopNameModel>(data);
        }
        public static void SetEmail()
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\Email.json");
            var data = "";
            if (System.IO.File.Exists(path))
            {
                data = System.IO.File.ReadAllText(path);
            }
            emailModel = JsonConvert.DeserializeObject<EmailModel>(data);
        }
        public static void SetProduct()
        {
            if (productModel!=null&&productModel.products.Count > 0)
            {
                return;
            }
            var list = new ProductModel();
            foreach (var itemSite in Utils.GetApp())
            {
                var product = Utils.GetDataFromLink("List_Product", "products", itemSite);
                productModel = JsonConvert.DeserializeObject<ProductModel>(product);
                if (list.products == null)
                {
                    list.products = productModel.products;
                }
                else
                {
                    list.products.AddRange(productModel.products);
                }
                
            }
            productModel = list;
        }
        public static string GetImageUrl(string title,string varianId, ProductModel productModel)
        {
           
                foreach (var itemProduct in productModel.products)
                {
                    
                    foreach (var itemImage in itemProduct.images)
                    {
                        foreach (var itemVari in itemImage.variant_ids)
                        {
                            if (itemVari.ToString() == varianId)
                            {
                                return itemImage.src;
                            }
                        }
                    }
                if (itemProduct.title == title)
                {
                    return itemProduct.images[0].src;
                }
            }
                return "";
        }
        public static List<ListSite> GetApp()
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\Site.json");
            var data = "";
            if (System.IO.File.Exists(path))
            {
                data = System.IO.File.ReadAllText(path);
            }

            var listSite = new List<ListSite>();
            listSite = JsonConvert.DeserializeObject<List<ListSite>>(data);
            return listSite;
        }
        public static string GetDataFromLink(string fileName,string nameItem, ListSite appModel)
        {
            var directory = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") + String.Format("\\Data\\{0}", appModel.Site);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var path = directory+ String.Format("\\{0}.json", fileName);
            if (!File.Exists(path))
            {
                File.Create(path).Close();
               
            }
            var data = "";
            try
            {
                WebClient client = new WebClient();
                client.UseDefaultCredentials = true;

                client.Credentials = new NetworkCredential(appModel.AppId, appModel.AppPass);
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(appModel.AppId + ":" + appModel.AppPass));
                client.Headers[HttpRequestHeader.Authorization] = string.Format(
                    "Basic {0}", credentials);
                data = client.DownloadString(String.Format("https://{0}.myshopify.com/admin/api/2019-07/{1}.json?limit=250", appModel.Site, nameItem));
                try
                {
                    using (var tw = new StreamWriter(path, false))
                    {
                        tw.WriteLine(data);
                        tw.Close();
                    }
                }
                catch { }
                
            }
            catch(Exception ex)
            {
                data = System.IO.File.ReadAllText(path);
            }
            
            return data;
        }
        public static bool CheckLink(string link)
        {
            bool result = false;
            //var a = client.DownloadString(link);


            var request = (HttpWebRequest)WebRequest.Create(link);
            request.UserAgent = "Mozilla/5.0 (Linux; <Android Version>; <Build Tag etc.>) AppleWebKit/<WebKit Rev> (KHTML, like Gecko) Chrome/<Chrome Rev> Mobile Safari/<WebKit Rev>";
            HttpClient client1 = new HttpClient();
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                var ba = 0;
            }

            return result;
        }
        public static List<String> GetDataFromFile(string fileName)
        {
            String data = "";
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") + String.Format("\\Data\\{0}.json", fileName);
            if (System.IO.File.Exists(path))
            {
                data = System.IO.File.ReadAllText(path);
            }

            var listOrder = new List<String>();
            listOrder = JsonConvert.DeserializeObject<List<String>>(data);
            if (listOrder == null)
            {
                listOrder = new List<string>();
            }
            return listOrder;
        }
        public static bool AddDataToFile(String inparam, string fileName)
        {
            try
            {
                var list = GetDataFromFile(fileName);
                list.Add(inparam);
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + String.Format("\\Data\\{0}.json",fileName));
                string value = JsonConvert.SerializeObject(list);
                using (var tw = new StreamWriter(path, false))
                {
                    tw.WriteLine(value);
                    tw.Close();
                }
            }
            catch (Exception ex)
            {
                
            }
            
            return true;
        }
        
    }
}
