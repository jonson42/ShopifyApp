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
    public class RedirectController : Controller
    {
        [HttpPost("updateHost")]
        public string UpdateHost([FromBody]List<ListHost> inparam)
        {
            try
            {
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\Data_Redirect.json");
                //var data = System.IO.File.ReadAllText(path + "\\Data\\Data_Redirect.json");
                string value = JsonConvert.SerializeObject(inparam);
                using (var tw = new StreamWriter(path, false))
                {
                    tw.WriteLine(value);
                }
            }catch(Exception ex)
            {
                return ex.ToString();
            }
            

            return "true";
        }
        //
        [HttpGet("getDefaultHost")]
        public List<ListHost> GetDefaultHost()
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\Data_Redirect.json");
            //var data = System.IO.File.ReadAllText(path + "\\Data\\Data_Redirect.json");
            var data = System.IO.File.ReadAllText(path);
            var listHost = new List<ListHost>();
            listHost = JsonConvert.DeserializeObject<List<ListHost>>(data);
            return listHost;
        }
        //Collection: 
        [HttpPost("updateCollection")]
        public string updateCollection([FromBody]List<ListHost> inparam)
        {
            try
            {
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\Collection.json");
                string value = JsonConvert.SerializeObject(inparam);
                using (var tw = new StreamWriter(path, false))
                {
                    tw.WriteLine(value);
                    tw.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }


            return "true";
        }
        //
        [HttpGet("getCollectionDefault")]
        public List<ListHost> getCollectionDefault()
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\Collection.json");
            var data = "";
            if (System.IO.File.Exists(path))
            {
                 data= System.IO.File.ReadAllText(path);
            }
            
            var listHost = new List<ListHost>();
            listHost = JsonConvert.DeserializeObject<List<ListHost>>(data);
            return listHost;
        }
        //Site: 
        [HttpPost("updateSite")]
        public string updateSite([FromBody]List<ListSite> inparam)
        {
            try
            {
                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\Site.json");
                string value = JsonConvert.SerializeObject(inparam);
                using (var tw = new StreamWriter(path, false))
                {
                    tw.WriteLine(value);
                    tw.Close();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "true";
        }
        //
        [HttpGet("getSiteDefault")]
        public List<ListSite> getSiteDefault()
        {
            return Utils.GetApp();
        }
    }
}
