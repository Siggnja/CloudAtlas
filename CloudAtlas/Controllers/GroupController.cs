using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudAtlas.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        
    }
}