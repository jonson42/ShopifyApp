using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace ShopifyBackupWeb.Apis
{
    [Route("/")]
    public class ShortLinkController: Controller
    {
        [HttpGet("products/{l}")]
        public RedirectResult shortLink(string l)
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\Data_Redirect.json");
            var data = System.IO.File.ReadAllText(path);
            var listHost = JsonConvert.DeserializeObject<List<Models.ListHost>>(data);
            for(int i = 0; i < listHost.Count; i++)
            {
                var link = listHost[i].Name + "products/" + l;
                var a = Utils.CheckLink(link);
                
                if (a)
                {
                    return Redirect(link);
                }
            }
            RedirectController redirectController = new RedirectController();
            var linkCall = redirectController.getLinkCallName();
            return Redirect(linkCall.Name);
        }
        [HttpGet("collections")]
        public RedirectResult shortLinkCollection(string l)
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + "\\Data\\Collection.json");
            var data = System.IO.File.ReadAllText(path);
            var listHost = JsonConvert.DeserializeObject<List<Models.ListHost>>(data);
            for (int i = 0; i < listHost.Count; i++)
            {
                var link = listHost[i].Name + "collections" + l;
                var a = Utils.CheckLink(link);

                if (a)
                {
                    return Redirect(link);
                }
            }
            return Redirect("https://www.google.com/");
        }

    }
}
