using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Controllers
{
    public class AbandonedController:Controller
    {
        public IActionResult Abandoned()
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return LocalRedirect("~/Home/Login");

            }
            return View();
        }

        public IActionResult AbandonedDetails()
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return LocalRedirect("~/Home/Login");

            }
            return View();
        }
    }
}
