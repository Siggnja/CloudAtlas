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
            var thisfile = (from file in context.Files
                            where file.ID == id
                            select file).FirstOrDefault();

            return Json(new { content = thisfile.Content },
                JsonRequestBehavior.AllowGet);
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

        [HttpGet]
        public ActionResult Create()
        {
            SelectLanguage();

            return View(new Project());
        }


        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                Project newProject = new Project();
                newProject.ID = project.ID;
                newProject.Name = project.Name;
                newProject.Description = project.Description;
                newProject.Type = project.Type;
                

                context.Projects.Add(newProject);
                context.SaveChanges();

                return RedirectToAction("Project", "Index", new { id = newProject.ID });
            }

            return View(project);
        }


        public void SelectLanguage()
        {
            IEnumerable<SelectListItem> items = new List<SelectListItem>() {

                new SelectListItem { Text = "Javascript", Value = "javascript" },

                new SelectListItem { Text = "HTML", Value = "html" },

                new SelectListItem { Text = "CSS", Value = "css" },

                new SelectListItem { Text = "C#", Value = "c#" },

                new SelectListItem { Text = "C++", Value = "c++" }
            };
            ViewData["Type"] = items;

        }


    }
}