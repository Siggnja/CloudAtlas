using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudAtlas.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using CloudAtlas.Repositories;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using Newtonsoft.Json.Linq;

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
                new SelectListItem { Text = "Midnight", Value = "clouds_midnight" },
                new SelectListItem { Text = "Sublime", Value = "monokai" },
                new SelectListItem { Text = "Eclipse", Value = "eclipse" },
                new SelectListItem { Text = "Github", Value = "github" },
                new SelectListItem { Text = "Terminal", Value = "terminal" }
            };
            ViewData["Theme"] = themes;
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {

            Account account = new Account(
                "cloudatlas",
                "215256475133816",
                "E6XXbAvQXKM05K9FqQpgxJ6ggQI");

            Cloudinary cloudinary = new Cloudinary(account);

            string userid = User.Identity.GetUserId<string>();

            var fileName = userid;
            var path = Path.Combine(Server.MapPath("~/Content/images/Avatars/"), fileName);

            file.SaveAs(path);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Server.MapPath("~/Content/images/Avatars/" + fileName)),
                PublicId = fileName
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            var curruser = (from user in db.Users
                            where user.Id == userid
                            select user).FirstOrDefault();


            curruser.AvatarPath = uploadResult.JsonObj["secure_url"].ToString();



            db.SaveChanges();

            return Json(new { status = "success" },
                JsonRequestBehavior.AllowGet);


            // Vista urli[ i db

            // Redirecta user til baka e[a eitthvaert



        }






    }
}