using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudAtlas.Controllers
{

    [Authorize]
    public class ProjectController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Project
        public ActionResult Index()
        {
            var model = (from item in context.Users
                        select item).FirstOrDefault();


            return View(model);
        }
    }
}