using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudAtlas.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;


namespace CloudAtlas.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId<string>();
            ApplicationDbContext context = new ApplicationDbContext();
            var proj = (from i in context.Projects
                        where i.ApplicationUserID == id
                        select i).ToList();
            var grou = (from i in context.Groups
                        where i.OwnerID == id
                        select i).ToList();

            DashboardViewModel model = new DashboardViewModel{ Projects = proj.ToList<Project>(), Groups = grou};
           
            return View(model);
        }

        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Project()
        {
            return View();
        }
    }
}