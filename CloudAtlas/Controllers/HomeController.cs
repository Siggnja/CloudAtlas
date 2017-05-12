using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudAtlas.Models;

namespace CloudAtlas.Controllers
{

    [RequireHttps]
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        { 
            return View();

            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
              return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult Login(string provider,string returnUrl)
        {
            return RedirectToAction("ExternalLogin","Account", new { provider = provider, returnurl = returnUrl });
        }
    }
}