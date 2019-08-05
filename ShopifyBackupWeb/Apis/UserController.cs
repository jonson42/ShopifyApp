using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopifyBackupWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Apis
{
    [Route("api/shopify")]
    public class UserController:ControllerBase
    {
        [HttpPost("Register")]
        public bool Register([FromBody]UserModel user)
        {
            string folderShop = Directory.GetCurrentDirectory()+ String.Format("\\wwwroot\\Data\\{0}",user.Shop);
            string fileUser = Directory.GetCurrentDirectory() + String.Format("\\wwwroot\\Data\\{0}","User.json");
            
            if (!Directory.Exists(folderShop))
            {
                Directory.CreateDirectory(folderShop);
            }
            var data = System.IO.File.ReadAllText(fileUser);
            var listUser = JsonConvert.DeserializeObject<List<UserModel>>(data)!=null?JsonConvert.DeserializeObject<List<UserModel>>(data):new List<UserModel>();
            listUser.Add(user);
            string value = JsonConvert.SerializeObject(listUser);
            using (var tw = new StreamWriter(fileUser, false))
            {
                tw.WriteLine(value);
            }
            return true;
        }

        [HttpPost("Login")]
        public bool Login([FromBody]UserModel user)
        {
            string fileUser = Directory.GetCurrentDirectory() + String.Format("\\wwwroot\\Data\\{0}", "User.json");
            var listUserStr = System.IO.File.ReadAllText(fileUser);
            var listUser = JsonConvert.DeserializeObject<List<UserModel>>(listUserStr);
            foreach (var item in listUser)
            {
                if (item.User==user.User&&item.Password==user.Password)
                {
                    HttpContext.Session.SetString("user",user.User);
                    HttpContext.Session.SetString("pass",user.Password);
                    HttpContext.Session.SetString("shop", item.Shop);
                    HttpContext.Session.SetString("appId", item.AppId);
                    HttpContext.Session.SetString("appPass", item.AppPass);
                   
                    return true;
                }
            }
            
            return false;
        }
    }
}
