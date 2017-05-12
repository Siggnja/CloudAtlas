using CloudAtlas.Models;
using CloudAtlas.Repositories;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudAtlas.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private readonly GroupsRepository groupsrepository;

        public GroupController()
        {
            groupsrepository = new GroupsRepository();
        }


        // GET: Group
        public ActionResult Index(int id)
        {
            var group = groupsrepository.GetGroupById(id);

            ViewData["userid"] = User.Identity.GetUserId<string>();


            return View(group);
        }

        public ActionResult Create()
        {
            return View(new Group());
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var name = collection["Name"];
            var usersString = collection["HiddenUsers"];
            var loggedInUser = User.Identity.GetUserId<string>();

            if(string.IsNullOrEmpty(name))
            {
                return View(new Group());
            }

            string[] users = usersString.Split(' ');
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>();

            foreach (var i in users)
            {
                var user = groupsrepository.GetUserByEmail(i);

                if (user != null)
                {
                    applicationUsers.Add(user);
                }
            }

            applicationUsers.Add(groupsrepository.GetUserByID(loggedInUser));


            Group newGroup = new Group { Name = name, OwnerID = loggedInUser, ApplicationUsers = applicationUsers };

            groupsrepository.AddGroup(newGroup);

            return RedirectToAction("Index", "Dashboard");

        }

        public JsonResult Search(string term)
        {
            var toSearch = new List<string>();

            string userID = User.Identity.GetUserId<string>();
            if (term != null)
            {
                toSearch = groupsrepository.SearchUserByEmail(term, userID);
            }
            else
            {
                var users = groupsrepository.GetUsers();

                toSearch = users.Select(m => m.Email).ToList();
            }

            return Json(toSearch, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Invite(FormCollection collection)
        {
            return View();
        }



        [HttpPost]
        public ActionResult AddUser(string email, int groupID)
        {
            var group = groupsrepository.GetGroupById(groupID);

            var curruser = groupsrepository.GetUserByEmail(email);
            if(curruser == null)
            {
                return Json(new { status = 3 },
                JsonRequestBehavior.AllowGet);
            }

            if(groupsrepository.UserInGroup(curruser, group))
            {
                return Json(new { status = 2 },
                JsonRequestBehavior.AllowGet);
            }

            groupsrepository.AddUserToGroup(curruser, group);

            return Json(new { status = 1 },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveUser(string email, int groupID)
        {
            var group = groupsrepository.GetGroupById(groupID);

            var curruser = groupsrepository.GetUserByEmail(email);

            if(!groupsrepository.UserInGroup(curruser, group))
            {
                return Json(new { status = "notingroup" },
                JsonRequestBehavior.AllowGet);
            }

            groupsrepository.RemoveUserFromGroup(curruser, group);

            return Json(new { status = "success" },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int groupID)
        {
            groupsrepository.DeleteGroupById(groupID);

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Leave(int groupID)
        {
            string userid = User.Identity.GetUserId<string>();
            var curruser = groupsrepository.GetUserByID(userid);

            var group = groupsrepository.GetGroupById(groupID);

            groupsrepository.RemoveUserFromGroup(curruser, group);


            return RedirectToAction("Index", "Dashboard");
        }

    }
}