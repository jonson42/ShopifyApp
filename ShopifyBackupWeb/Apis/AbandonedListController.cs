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
            var listCheckOut = new List<Checkout>();
            //link api :
            foreach (var itemSite in Utils.GetApp())
            {
                var data = Utils.GetDataFromLink("List_Abandoned", "checkouts",itemSite);
                AbandonedModel abandonedModel = new AbandonedModel();
                abandonedModel = JsonConvert.DeserializeObject<AbandonedModel>(data);
                if (abandonedModel!=null&&abandonedModel.checkouts != null)
                {
                    foreach (var itemCheckOut in abandonedModel.checkouts)
                    {
                        listCheckOut.Add(itemCheckOut);
                    }
                }
                
            }
            return listCheckOut;
        }

        [HttpGet("getAbandonedDetails")]
        public Checkout GetAbandonedDetails(string idProduct)
        {
            Checkout itemCheck = new Checkout();
            //link api : 
            foreach (var itemSite in Utils.GetApp())
            {
                var data = Utils.GetDataFromLink("List_Abandoned", "checkouts", itemSite);
                AbandonedModel abandonedModel = new AbandonedModel();
                abandonedModel = JsonConvert.DeserializeObject<AbandonedModel>(data);
                if (abandonedModel != null && abandonedModel.checkouts != null)
                {
                    foreach (var item in abandonedModel.checkouts)
                    {
                        if (item.id.ToString() == idProduct)
                        {
                            itemCheck = item;
                            break;
                        }
                    }
                }
            }
            return itemCheck;
        }
    }
}
