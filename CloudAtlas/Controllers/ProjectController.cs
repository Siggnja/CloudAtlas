using CloudAtlas.Models;
using Microsoft.AspNet.Identity;
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
        public ActionResult Index(int id)
        {
            
            var project = (from proj in context.Projects
                           where proj.ID == id
                           select proj).FirstOrDefault();
   
            var root = (from fold in context.Folders
                        where fold.ID == project.FolderID
                        select fold).FirstOrDefault();

            var folders = (from fold in context.Folders
                           where fold.ParentID == root.ID
                           select fold).ToList();

            var files = (from file in context.Files
                         where file.FolderID == root.ID
                         select file).ToList();
            
            ProjectViewModel model = new ProjectViewModel
            {
                Project = project,
                Root = root,
                Folders = folders,
                Files = files
            };


            string userid = User.Identity.GetUserId<string>();
            var useremail = (from user in context.Users
                         where user.Id == userid
                         select user.Email).FirstOrDefault();

            ViewData["UserEmail"] = useremail;
            return View(model);
        }

        public ActionResult OpenFile(int id)
        {

       

            return View();
        }


        public ActionResult SaveFile(FormCollection collection)
        {
            var code = collection["hiddencode"];
            var id = Int32.Parse(collection["hiddenid"]);

            var thisfile = (from file in context.Files
                            where file.ID == id
                            select file).FirstOrDefault();

            thisfile.Content = code;

            context.SaveChanges();

            var projectid = Int32.Parse(collection["hiddenproject"]);

            return RedirectToAction("Index", "Project", new { id = projectid});
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}