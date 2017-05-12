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
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly ProjectsRepository projrepository;
        private readonly FolderRepository folderrepository;
        private readonly FileRepository filerepository;

        public ProjectController()
        {
            projrepository = new ProjectsRepository(db);
            folderrepository = new FolderRepository(db);
            filerepository = new FileRepository(db);
        }

        // GET: Project
        /// <summary>
        /// Gets all the folders and files associated with the project and sends it to the view 
        /// </summary>
        public ActionResult Index(int id)
        {
            var project = projrepository.GetProjectById(id);

            var root = folderrepository.GetFolderByID(project.FolderID);

            var folders = folderrepository.GetFoldersFromRootID(id);

            var files = filerepository.GetFilesByFolderID(root.ID);

            string userId = User.Identity.GetUserId<string>();

            var curruser = projrepository.GetUserByID(userId);

            ViewData["userId"] = userId;


            ProjectViewModel model = new ProjectViewModel
            {
                Project = project,
                Root = root,
                User = curruser,
                Folders = folders,
                Files = files
            };

            SelectLanguage();


            return View(model);
        }
        

        public ActionResult OpenFile(int id)
        {
            var thisfile = filerepository.GetFileByID(id);

            return Json(new { content = thisfile.Content, type = thisfile.Type, name = thisfile.Name },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RenameFile(int? id, string newName, int projectId)
        {
            if (id == null)
            {
                return View();
            }
            File changeFile = filerepository.GetFileById((int)id);
            foreach(var fil in folderrepository.GetAllFilesById(changeFile.FolderID))
            {
                if(fil.Name == newName)
                {
                    return InitilizeTree(projectId);
                }
            }


            filerepository.UpdateFileNameByID((int)id, newName);
            return InitilizeTree(projectId);
        }
        [HttpPost]
       
        public ActionResult RenameFolder(int? id, string newName, int projectId)
        {
            if (id == null)
            {
                return View();
            }
            Folder changeFold = folderrepository.GetFolderByID((int)id);
            foreach(var temp in changeFold.Parent.SubFolders)
            {
                if(temp.Name == newName)
                {
                    return RedirectToAction("Index", "Project", new { id = projectId });
                }
            }
            folderrepository.UpdateFolderNameByID((int)id, newName);
            
            return RedirectToAction("Index", "Project", new { id = projectId });

        }
        
        [HttpPost]
        public ActionResult RemoveFile(int? id, int projectId)
        {
            if (id == null)
            {
                return View();
            }
            filerepository.RemoveFileByID((int)id);

            return RedirectToAction("Index", "Project", new { id = projectId });

        }
        
        [HttpPost]
        public ActionResult RemoveFolder(int? id, int projectId)
        {
            if (id == null)
            {
                return View();
            }
            folderrepository.RemoveFolderByID((int)id);
            return RedirectToAction("Index", "Project", new { id = projectId });

        }
        /// <summary>
        /// Initializes a Jstree with appropriate folder and files in the project and returns a json string
        /// </summary>
        public ActionResult InitilizeTree(int id)
        {
            var nodes = new List<JsTreeModel>();
            var project = projrepository.GetProjectById(id);
            var root = folderrepository.GetFolderByID(project.FolderID);
            nodes.Add(new JsTreeModel() { id = root.ID.ToString(), parent = "#", text = root.Name, type = "folder" });
            AddChildren(nodes, root);
            AddChildrenFiles(nodes, root);

            return Json(nodes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddFileInput()
        {
            SelectLanguage();
            return PartialView("CreateFileInput", new File());
        }
        /// <summary>
        /// Creates a new file with a information from a input form and saves it to the database
        /// </summary>
        [HttpPost]
        public ActionResult AddFile(FormCollection collection)
        {
            var temp = collection["hiddenparent"];
            int? parentId = int.Parse(temp.ToString());
            if (parentId == null)
            {
                return View();
            }
            int projectId = int.Parse(collection["hiddenproject"]);
            Project proj = projrepository.GetProjectById(projectId);
            foreach(var fil in folderrepository.GetFolderByID((int)parentId).Files)
            {
                if(fil.Name == collection["name"])
                {
                    return RedirectToAction("Index", "Project", new { id = projectId });
                }
            }
           
            string extension = GetExtension(collection["Type"].ToString());
            Folder par = folderrepository.GetFolderByID((int)parentId);
            File newFile = new Models.File { Name = collection["name"], Content = "", FolderID = par.ID, Type = collection["Type"], Extension = extension };
            filerepository.AddFile(newFile);

           return RedirectToAction("Index", "Project", new { id = projectId });

        }
        private string GetExtension(string type)
        {
            if (type == "javascript") return ".js";
            if (type == "css") return ".css";
            if (type == "csharp") return ".cs";
            if (type == "c_cpp") return ".cpp";
            if (type == "html" ) return ".html";
            return "";
        }
        /// <summary>
        /// Adds SubFolders and Files to a folder
        /// </summary>
        public void addChildren(List<JsTreeModel> nodes, Folder root)
        {
            if(root != null && root.SubFolders != null)
            {
                foreach (Folder f in root.SubFolders)
                {
                    nodes.Add(new JsTreeModel() { id = f.ID.ToString(), parent = root.ID.ToString(), text = f.Name, type = "folder" });
                    AddChildren(nodes, f);
                    AddChildrenFiles(nodes, f);
                }
            }
        }
        /// <summary>
        /// Add Files to a folder
        /// </summary>
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
        /// <summary>
        /// Removes a file and updates the database  
        /// </summary>
        [HttpPost]
        public ActionResult AddFolder(int? parentId, int projectId)
        {
            if (parentId == null)
            {
                return View();
            }
            Folder par = folderrepository.GetFolderByID((int)parentId);
            foreach(var res in par.SubFolders)
            {
                if (res.Name == "New Folder")
                {
                    return View();
                }
            }
            Folder newFold = new Folder { Name = "New Folder", IsRoot = false, ParentID = parentId };
            folderrepository.AddFolder(newFold, par);

            return InitilizeTree(projectId);

        }


        public ActionResult SaveFile(FormCollection collection)
        {
            var code = collection["hiddencode"];
            var id = Int32.Parse(collection["hiddenid"]);

            var projectid = Int32.Parse(collection["hiddenproject"]);

            return RedirectToAction("Index", "Project", new { id = projectid});
        }

        [ValidateInput(false)]
        public ActionResult SaveFileAuto(string code, int id, int projectid)
        {

            filerepository.SaveFile(id, code);


            return RedirectToAction("Index", "Project", new { id = projectid });
        }

        [HttpGet]
        public ActionResult Create()
        {
            SelectLanguage();

            return View(new Project());
        }

        /// <summary>
        /// Takes in search string, and searches both users and groups
        /// returns a list of users and groups
        /// </summary>
        public JsonResult Search(string term)
        {
            string loggedInUser = User.Identity.GetUserId<string>();

            var users = projrepository.SearchUserByEmail(term, loggedInUser);

            var groups = projrepository.SearchGroupByName(loggedInUser, term);

            var listName = users.Union(groups);
                
            return Json(listName, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///  Invites a user to the current project
        /// </summary>
        public ActionResult Invite(FormCollection collection)
        {
            var stringToCheck = collection["UserEmail"];
            //gets the ID of the project wich is stored in a hidden input in the form
            var hidden = collection["hiddenproject"];
            int projectID = Int32.Parse(hidden);
            string loggedInUser = User.Identity.GetUserId<string>();

            var project = projrepository.GetProjectById(projectID);

            var user = projrepository.GetUserByID(loggedInUser);

            var checkGroup = (from g in user.Groups
                              where g.Name == stringToCheck
                              select g).FirstOrDefault();


            //Checks if groups exist, if so adds all the users to the project
            //from the group that aren't in the project
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

            var users = projrepository.GetUsers();

            var checkUser = (from item in users
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


        /// <summary>
        /// Creates a project with a root folder and an initial file
        /// </summary>
       
        [HttpPost]
        public ActionResult Create(Project project)
        {
            Folder newfold = new Folder { Name = "Root", IsRoot = true };

            folderrepository.AddFolder(newfold);

            var foldid = newfold.ID;

            File newfile = CreateInitialFile(foldid, project);

            if(project.Type == "javascript")
            {
                filename = "Index";
                fileext = ".js";
                filecont = "function toScreen(hello)\n{\n\tdocument.write(hello);\n}\nvar text = \"Hello World\";\ntoScreen(text);";
            }
            else if(project.Type == "html")
            {
                filename = "Index";
                fileext = ".html";
                filecont = "<!DOCTYPE html>\n<html lang=\"en\">\n\n\t<head>\n\t\t<meta charset=\"UTF-8\">\n\t\t<title>Hello!</title>\n\t</head>\n\n\t<body>\n\t\t<h1>Hello World!</h1>\n\t\t<p>You can edit me, and so can your friends!</p>\n\t</body>\n\n</html>";
            }
            else if(project.Type == "css")
            {
                filename = "Index";
                fileext = ".css";
                filecont = "#id\n{\n\tcolor: DeepSkyBlue;\n}";
            }
            else if(project.Type == "csharp")
            {
                filename = "Main";
                fileext = ".cs";
                filecont = "public class Hello\n{\n\tpublic static void Main()\n\t{\n\t\tSystem.Console.WriteLine(\"Hello, World!\");\n\t}\n}";
            }
            else
            {
                filename = "Main";
                fileext = ".cpp";
                filecont = "#include <iostream>\n\nusing namespace std;\n\nint main()\n{\n\tcout << \"Hello World!\";\n}";
            }

            File newfile = new File
            {
                FolderID = foldid,
                Name = filename,
                Extension = fileext,
                Type = project.Type,
                Content = filecont
            };

            filerepository.AddFile(newfile);

            string userid = User.Identity.GetUserId<string>();

            Project newProject = new Project()
            {
                Name = project.Name,
                Type = project.Type,
                FolderID = foldid,
                OwnerID = userid
            };


            var curruser = projrepository.GetUserByID(userid);


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
            var curruser = projrepository.GetUserByID(userid);

            var project = projrepository.GetProjectById(projectid);

            projrepository.DeleteFromProject(project, curruser);

            return RedirectToAction("Index", "Dashboard");
        }

        /// <summary>
        /// Downloads a single file of the file type
        /// </summary>

        public ActionResult Download(int fileID)
        {
            var currentfile = filerepository.GetFileById(fileID);
         

            return File(Encoding.UTF8.GetBytes(currentfile.Content),
                 "text/plain",
                  string.Format("{0}{1}", currentfile.Name, currentfile.Extension));
        }

        /// <summary>
        /// Populates type dropdown list in create view
        /// </summary>
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

        /// <summary>
        /// Takes in the project, and folder id, and creates the initial file
        /// of the correspondant type
        /// </summary>
        private File CreateInitialFile(int foldid, Project project)
        {
            var filecont = "";
            var fileext = "";
            var filename = "";

            if (project.Type == "javascript")
            {
                filename = "Index";
                fileext = ".js";
                filecont = "var x = \"Hello World\";";
            }
            else if (project.Type == "html")
            {
                filename = "Index";
                fileext = ".html";
                filecont = "<html>\n< header >< title > This is title </ title ></ header >\n< body >\nHello world\n</ body >\n</ html > ";
            }
            else if (project.Type == "css")
            {
                filename = "Index";
                fileext = ".css";
                filecont = "#id{\n color: DeepSkyBlue;}";
            }
            else if (project.Type == "csharp")
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

            return newfile;
        }
    }
}