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
        public static string User { get; set; }
        public static string Password { get; set; }
        public static string AppId { get; set; }
        public static string AppPass { get; set; }
        public static string Shop { get; set; }

        public static void SetUser(UserModel user)
        {
            User = user.User;
            Password = user.Password;
            AppId = user.AppId;
            AppPass = user.AppPass;
            Shop = user.Shop;
        }
        public static string GetDataFromLink(string fileName,string nameItem)
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")+ String.Format("\\Data\\{0}\\{1}.json", Shop, fileName);
            if (!File.Exists(path))
            {
                File.Create(path).Close();
               
            }
            var data = "";
            try
            {
                WebClient client = new WebClient();
                client.UseDefaultCredentials = true;

                client.Credentials = new NetworkCredential(AppId, AppPass);
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(AppId + ":" + AppPass));
                client.Headers[HttpRequestHeader.Authorization] = string.Format(
                    "Basic {0}", credentials);
                data = client.DownloadString(String.Format("https://{0}.myshopify.com/admin/api/2019-07/{1}.json", Shop, nameItem));
                using (var tw = new StreamWriter(path, false))
                {
                    tw.WriteLine(data);
                    tw.Close();
                }
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
    }
}
