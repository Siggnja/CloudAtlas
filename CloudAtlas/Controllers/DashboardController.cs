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
        private readonly Account account;
        private readonly Cloudinary cloudinary;

        public DashboardController()
        {
            projectsRepository = new ProjectsRepository(db);
            groupsRepository = new GroupsRepository(db);

            //Creates the connection to cloudinary, an online image host
            account = new Account(
                    "cloudatlas",
                    "215256475133816",
                    "E6XXbAvQXKM05K9FqQpgxJ6ggQI");
            cloudinary = new Cloudinary(account);
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId<string>();
            var proj = projectsRepository.GetProjectsByUserId(id);
            var grou = groupsRepository.getAllGroupsByUserId(id);
            DashboardViewModel model = new DashboardViewModel{ Projects = proj, Groups = grou};

            ViewData["userid"] = id;

            return View(model);
        }

        /// <summary>
        /// Finds the settings for the current user and returns the correct view
        /// </summary>

        public ActionResult Settings()
        {
            string userid = User.Identity.GetUserId<string>();

            var curruser = (from user in db.Users
                            where user.Id == userid
                            select user).FirstOrDefault();

            SelectTheme();


            return View(curruser);
        }

        public ActionResult Project()
        {
            return View();
        }

        /// <summary>
        /// Takes in the user's username and theme, and saves them to the database
        /// </summary>

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

        public void SelectTheme()
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

        /// <summary>
        /// Takes in a image file, uploads it to the web server,
        /// then uploads it to cloudinary image host and deletes it from the web server.
        /// Saves the path to cloudinary image in database.
        /// </summary>

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            string userid = User.Identity.GetUserId<string>();

            if (file != null)
            {
                var fileName = userid;
                var path = Path.Combine(Server.MapPath("~/Content/images/Avatars/"), fileName);

                //Saves the file to the web server
                file.SaveAs(path);

                //Creates the image file for cloudinary, and links the users ID to the filename.
                //Then transforms the image and crops it.
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(Server.MapPath("~/Content/images/Avatars/" + fileName)),
                    PublicId = fileName,
                 Transformation =  (new Transformation()
                .Width(280).Height(280).Gravity("face").Radius("max").Crop("fill").Chain())                
        
                };

                //Upload image to cloudinary
                var uploadResult = cloudinary.Upload(uploadParams);

                //delete from web server
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var curruser = (from user in db.Users
                                where user.Id == userid
                                select user).FirstOrDefault();

                
                //finds the cloudinary url from a Jtoken object and saves it in the database
                curruser.AvatarPath = uploadResult.JsonObj["secure_url"].ToString();

                db.SaveChanges();

                return RedirectToAction("Settings", "Dashboard");

            }
            var userCurr = (from u in db.Users
                            where u.Id == userid
                            select u).FirstOrDefault();

            SelectTheme();


            return View("Settings", userCurr);

        }






    }
}