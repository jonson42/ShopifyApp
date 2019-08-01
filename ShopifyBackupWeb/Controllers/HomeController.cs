using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopifyBackupWeb.Models;

namespace ShopifyBackupWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Details()
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Refund()
        {
            return View();
        }
       
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return LocalRedirect("~/Home/Login");
                
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
