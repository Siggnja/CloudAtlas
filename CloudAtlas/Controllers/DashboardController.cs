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
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            /*
            List<Project> proj = new List<Project>
            {
                new Project{ID = 0, Name = "Hello World"},
                new Project{ID = 1, Name = "Project X"},
                new Project{ID = 2, Name = "Facebook 2.0"},
                new Project{ID = 4, Name = "Verklegt 2"}
            };
            List<Group> grou = new List<Group>
            {
                new Group{ID = 0, Name = "Gagnaskipan"},
                new Group{ID = 1, Name = "Vefforritun"},
                new Group{ID = 2, Name = "Ramadan"},
                new Group{ID = 3, Name = "Allah"}
            };
            */

            string id = User.Identity.GetUserId<string>();
            ApplicationDbContext context = new ApplicationDbContext();
            var proj = (from i in context.Projects
                        where i.ApplicationUserID == id
                        select i).ToList() ;
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