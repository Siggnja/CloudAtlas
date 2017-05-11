using CloudAtlas.Models;
using CloudAtlas.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO.Compression;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

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

            string userid = User.Identity.GetUserId<string>();
            var curruser = (from user in context.Users
                            where user.Id == userid
                            select user).FirstOrDefault();
            ViewData["userId"] = userid;


            ProjectViewModel model = new ProjectViewModel
            {
                Project = project,
                Root = root,
                User = curruser,
                Folders = folders,
                Files = files
            };
            
            return View(model);
        }

        public ActionResult OpenFile(int id)
        {
            var thisfile = (from file in context.Files
                            where file.ID == id
                            select file).FirstOrDefault();

            return Json(new { content = thisfile.Content, type = thisfile.Type },
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RenameFile(int? id, string newName, int projectId)
        {
            if (id == null)
            {
                return View();
            }
            var file = filerepository.getFileById((int)id);
            file.Name = newName.Trim();
            context.SaveChanges();
            return InitilizeTree(projectId);
        }
        [HttpPost]
        public ActionResult RenameFolder(int? id, string newName, int projectId)
        {
            if (id == null)
            {
                return View();
            }
            var folder = foldrepository.GetFolderByID((int)id);
            folder.Name = newName.Trim();
            context.SaveChanges();
            return RedirectToAction("Index", "Project", new { id = projectId });

        }
        [HttpPost]
        public ActionResult RemoveFile(int? id, int projectId)
        {
            if (id == null)
            {
                return View();
            }
            var file = filerepository.getFileById((int)id);
            foldrepository.removeFileFromFolder(file.FolderID, file);
            filerepository.deleteFile(file);

            context.SaveChanges();
            return RedirectToAction("Index", "Project", new { id = projectId });

        }
        [HttpPost]
        public ActionResult RemoveFolder(int? id, int projectId)
        {
            if (id == null)
            {
                return View();
            }
            Folder deleteMe = foldrepository.GetFolderByID((int)id);
            Folder parent = deleteMe.Parent;

            if (parent != null)
            {
                parent.SubFolders.Remove(deleteMe);
            }
            DeleteFolderHelper(deleteMe);


            foldrepository.removeFolder(deleteMe);

            context.SaveChanges();
            return RedirectToAction("Index", "Project", new { id = projectId });

        }
        private void DeleteFolderHelper(Folder deleteMe)
        {
            if(deleteMe != null && deleteMe.SubFolders != null)
            {
                for(int i = 0;i<deleteMe.SubFolders.Count;i++)
                {
                    DeleteFolderHelper(deleteMe.SubFolders.ElementAt(i));
                    DeleteAllFiles(deleteMe.SubFolders.ElementAt(i));
                    foldrepository.removeFolder(deleteMe.SubFolders.ElementAt(i));
                }
            }
        }
        private void DeleteAllFiles(Folder fold)
        {
            if(fold.Files != null)
            {
                foreach (File file in fold.Files)
                {
                    filerepository.deleteFile(file);
                }
            }
        }
        public ActionResult DeleteFile(int? idFile)
        {
            if (idFile == null)
            {
                return View();
            }
            filerepository.deleteFile(filerepository.getFileById((int)idFile));
            return View();
        }
        public ActionResult TreeView(int id)
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
            return PartialView("TreeView", model);
        }
        public ActionResult InitilizeTree(int id)
        {
            var nodes = new List<JsTreeModel>();
            Debug.WriteLine(id);
            var project = projrepository.GetProjectById(id);
            var root = foldrepository.GetFolderByID(project.FolderID);
            nodes.Add(new JsTreeModel() { id = root.ID.ToString(), parent = "#", text = root.Name, type = "folder" });
            addChildren(nodes, root);
            addChildrenFiles(nodes, root);

            return Json(nodes, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult CreateFileInput()
        {
            return PartialView("CreateFileInput", new File());
        }
        [HttpPost]
        public ActionResult CreateFile(FormCollection collection, int? parentId, int projectId)
        {

            if (parentId == null)
            {
                return View();
            }
            string extension = getExtension(collection["file.type"]);
            Folder par = foldrepository.GetFolderByID((int)parentId);
            File newFile = new Models.File { Name = collection["name"], Content = "", FolderID = par.ID, Type = collection["file.type"], Extension = extension };
            filerepository.addFile(newFile);

            context.SaveChanges();
            return InitilizeTree(projectId);

        }
        private string getExtension(string type)
        {
            if (type == "Javascript") return ".js";
            if (type == "CSS") return ".css";
            if (type == "C#") return ".cs";
            if (type == "C++") return ".cpp";
            return "";
        }
        public void addChildren(List<JsTreeModel> nodes, Folder root)
        {
            if(root != null && root.SubFolders != null)
            {
                foreach (Folder f in root.SubFolders)
                {
                    nodes.Add(new JsTreeModel() { id = f.ID.ToString(), parent = root.ID.ToString(), text = f.Name, type = "folder" });
                    addChildren(nodes, f);
                    addChildrenFiles(nodes, f);
                }
            }
        }
        private void addChildrenFiles(List<JsTreeModel> nodes,Folder fold)
        {
            if(fold != null && fold.Files != null)
            {
                foreach (File f in fold.Files)
                {
                    var temp = new { fileid = f.ID.ToString() };
                    var json = JsonConvert.SerializeObject(temp);
                    nodes.Add(new JsTreeModel() { id = f.ID.ToString(), parent = fold.ID.ToString(), text = f.Name + f.Extension, li_attr = temp.ToString(), type = "file" });
                }
            }
        }
        [HttpPost]
        public ActionResult CreateFolder(int? parentId, int projectId)
        {
            if (parentId == null)
            {
                return View();
            }
            Folder par = foldrepository.GetFolderByID((int)parentId);
            Folder newFold = new Folder{ Name="New Folder",IsRoot=false,ParentID=parentId};
            foldrepository.addFolder(newFold);
            par.SubFolders.Add(newFold);

            context.SaveChanges();
            return InitilizeTree(projectId);

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

        [ValidateInput(false)]
        public ActionResult SaveFileAuto(string code, int id, int projectid)
        {

            var thisfile = (from file in context.Files
                            where file.ID == id
                            select file).FirstOrDefault();

            thisfile.Content = code;

            context.SaveChanges();


            return RedirectToAction("Index", "Project", new { id = projectid });
        }

        [HttpGet]
        public ActionResult Create()
        {
            SelectLanguage();

            return View(new Project());
        }

        public JsonResult Search(string term)
        {
            string loggedInUser = User.Identity.GetUserId<string>();

            var users = (from user in context.Users
                            where user.Email.StartsWith(term)
                            select user.Email).ToList();

            var userGroup = (from user in context.Users
                             where user.Id == loggedInUser
                             select user.Groups).FirstOrDefault();

            var groups = (from gr in userGroup
                          where gr.Name.ToLower().StartsWith(term.ToLower())
                          select gr.Name).ToList();

            var listName = users.Union(groups);
                
            

            return Json(listName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Invite(FormCollection collection)
        {
            var stringToCheck = collection["UserEmail"];
            var hidden = collection["hiddenproject"];
            int projectID = Int32.Parse(hidden);
            string loggedInUser = User.Identity.GetUserId<string>();

            var project = (from i in context.Projects
                           where i.ID == projectID
                           select i).FirstOrDefault();

            var user = (from item in context.Users
                        where item.Id == loggedInUser
                        select item).FirstOrDefault();

            var checkGroup = (from g in user.Groups
                              where g.Name == stringToCheck
                              select g).FirstOrDefault();

            if(checkGroup != null)
            {
                foreach(var item in checkGroup.ApplicationUsers)
                {
                    if (!item.Projects.Contains(project))
                    {
                        projrepository.AddProjectToUser(project, item);
                    }
                }
            }

            var checkUser = (from item in context.Users
                             where item.Email == stringToCheck
                             select item).FirstOrDefault();

            if(checkUser != null)
            {
                if (!checkUser.Projects.Contains(project))
                {
                    projrepository.AddProjectToUser(project, checkUser);
                }
            }

            ProjectViewModel model = new ProjectViewModel
            {
                Project = project
            };

            return PartialView("Avatars", model);
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
            else if(project.Type == "csharp")
            {
                filename = "Main";
                fileext = ".cs";
                filecont = "public class Hello1\n{\n\tpublic static void Main()\n\t{\n\t\tSystem.Console.WriteLine(\"Hello, World!\");\n\t}\n}";
            }
            else
            {
                filename = "Main";
                fileext = ".cpp";
                filecont = "#include <iostream>\n\nint main()\n{\n\tstd::cout << \"Hello World!\";\n}";
            }

            File newfile = new File
            {
                FolderID = foldid,
                Name = filename,
                Extension = fileext,
                Type = project.Type,
                Content = filecont
            };

            filerepository.addFile(newfile);

            string userid = User.Identity.GetUserId<string>();

            Project newProject = new Project()
            {
                Name = project.Name,
                Type = project.Type,
                IsGroupProject = false,
                FolderID = foldid,
                OwnerID = userid
            };
            

            var curruser = (from user in context.Users
                            where user.Id == userid
                            select user).FirstOrDefault();


            projrepository.AddProject(newProject);

            projrepository.AddProjectToUser(newProject, curruser);

                

            return RedirectToAction("Index", "Project", new { id = newProject.ID });

        }

        public ActionResult Delete(int projectid)
        {
            var project = projrepository.GetProjectById(projectid);

            projrepository.RemoveProject(project);

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Leave(int projectid)
        {
            string userid = User.Identity.GetUserId<string>();
            var curruser = (from user in context.Users
                            where user.Id == userid
                            select user).FirstOrDefault();

            var project = projrepository.GetProjectById(projectid);

            projrepository.DeleteFromProject(project, curruser);

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Download(int fileID)
        {

            var archive = Server.MapPath("~/archive.zip");
            var currentfile = filerepository.getFileById(fileID);

            if (System.IO.File.Exists(archive))
            {
                System.IO.File.Delete(archive);
            }
            

            return File(Encoding.UTF8.GetBytes(currentfile.Content),
                 "text/plain",
                  string.Format("{0}{1}", currentfile.Name, currentfile.Extension));
        }


        public void SelectLanguage()
        {
            IEnumerable<SelectListItem> items = new List<SelectListItem>() {

                new SelectListItem { Text = "Javascript", Value = "javascript" },

                new SelectListItem { Text = "HTML", Value = "html" },

                new SelectListItem { Text = "CSS", Value = "css" },

                new SelectListItem { Text = "C#", Value = "csharp" },

                new SelectListItem { Text = "C++", Value = "c_cpp" }
            };
            ViewData["Type"] = items;

        }
    }
}