﻿using CloudAtlas.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudAtlas.Repositories;

namespace CloudAtlas.Controllers
{

    [Authorize]
    public class ProjectController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        private readonly ProjectsRepository projrepository;
        private readonly FolderRepository foldrepository;
        private readonly FileRepository filerepository;

        public ProjectController()
        {
            projrepository = new ProjectsRepository(context);
            foldrepository = new FolderRepository(context);
            filerepository = new FileRepository(context);
        }

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
            
            Folder newfold = new Folder { Name = "Root", IsRoot = true };

            foldrepository.addFolder(newfold);

            var foldid = newfold.ID;

            var filecont = "";
            var fileext = "";
            var filename = "";

            if(project.Type == "javascript")
            {
                filename = "Index";
                fileext = ".js";
                filecont = "var x = \"Hello World\";";
            }
            else if(project.Type == "html")
            {
                filename = "Index";
                fileext = ".html";
                filecont = "<html>\n< header >< title > This is title </ title ></ header >\n< body >\nHello world\n</ body >\n</ html > ";
            }
            else if(project.Type == "css")
            {
                filename = "Index";
                fileext = ".css";
                filecont = "#id{\n color: DeepSkyBlue;}";
            }
            else if(project.Type == "c#")
            {
                filename = "Main";
                fileext = ".cs";
                filecont = "public class Hello1\n{\npublic static void Main()\n{\nSystem.Console.WriteLine(\"Hello, World!\");\n}\n}";
            }
            else
            {
                filename = "Main";
                fileext = ".cpp";
                filecont = "#include <iostream>\n\nint main()\n{\nstd::cout << \"Hello World!\";\n}";
            }

            File newfile = new File
            {
                FolderID = foldid,
                Name = filename,
                Extension = fileext,
                Content = filecont
            };

            filerepository.addFile(newfile);

            Project newProject = new Project()
            {
                Name = project.Name,
                Type = project.Type,
                IsGroupProject = false,
                FolderID = foldid
            };
            string userid = User.Identity.GetUserId<string>();

            var curruser = (from user in context.Users
                            where user.Id == userid
                            select user).FirstOrDefault();


            projrepository.AddProject(newProject);

            projrepository.AddProjectToUser(newProject, curruser);

                

            return RedirectToAction("Index", "Project", new { id = newProject.ID });

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