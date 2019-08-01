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
    public class AbandonedListController : Controller
    {
        [HttpGet("getAbandoned")]
        public List<Checkout> GetListAbandoned()
        {
            //link api :
            var data = Utils.GetDataFromLink("List_Abandoned", "checkouts");
            AbandonedModel abandonedModel = new AbandonedModel();
            abandonedModel = JsonConvert.DeserializeObject<AbandonedModel>(data);
            return abandonedModel.checkouts;
        }

        [HttpGet("getAbandonedDetails")]
        public Checkout GetAbandonedDetails(string idProduct)
        {
            //link api : 
            var data = Utils.GetDataFromLink("List_Abandoned", "checkouts");
            AbandonedModel abandonedModel = new AbandonedModel();
            abandonedModel = JsonConvert.DeserializeObject<AbandonedModel>(data);
            Checkout itemCheck = new Checkout();
            foreach (var item in abandonedModel.checkouts)
            {
                if (item.id.ToString() == idProduct)
                {
                    itemCheck = item;
                    break;
                }
            }
            return itemCheck;
        }
    }
}
