using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudAtlas.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using CloudAtlas.Repositories;

namespace CloudAtlas.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly ProjectsRepository projectsRepository;
        public DashboardController()
        {
            projectsRepository = new ProjectsRepository(db);

        }
        // GET: Dashboard
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId<string>();
            ApplicationDbContext context = new ApplicationDbContext();
            var proj = projectsRepository.GetProjectByUserId(id);
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