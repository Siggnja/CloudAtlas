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
        private readonly GroupsRepository groupsRepository;

        public DashboardController()
        {
            projectsRepository = new ProjectsRepository(db);
            groupsRepository = new GroupsRepository(db);
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId<string>();
            var proj = projectsRepository.GetProjectsByUserId(id);
            var grou = groupsRepository.getAllGroupsByUserId(id);
            DashboardViewModel model = new DashboardViewModel{ Projects = proj.ToList<Project>(), Groups = grou};
           
            return View(model);
        }

        public ActionResult Settings()
        {
            string userid = User.Identity.GetUserId<string>();

            var curruser = (from user in db.Users
                            where user.Id == userid
                            select user).FirstOrDefault();

            SelectTheme(curruser);


            return View(curruser);
        }

        public ActionResult Project()
        {
            return View();
        }

        public ActionResult SaveSettings(string UserName, string theme)
        {
            string userid = User.Identity.GetUserId<string>();

            var curruser = (from user in db.Users
                            where user.Id == userid
                            select user).FirstOrDefault();

            curruser.UserName = UserName;
            curruser.Theme = theme;

            db.SaveChanges();

            return Json(new { status = "success" },
                JsonRequestBehavior.AllowGet);
        }

        public void SelectTheme(ApplicationUser user)
        {
            IEnumerable<SelectListItem> themes = new List<SelectListItem>() {

                new SelectListItem { Text = "Light", Value = "dawn" },
                new SelectListItem { Text = "Dark", Value = "chaos" },
                new SelectListItem { Text = "Sublime", Value = "monokai" }
            };
            ViewData["Theme"] = themes;
        }
    }
}