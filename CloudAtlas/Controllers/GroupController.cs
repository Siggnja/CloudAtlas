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
        private readonly ApplicationDbContext context;
        private readonly GroupsRepository groupsrepository;

        public GroupController()
        {
            context = new ApplicationDbContext();

            groupsrepository = new GroupsRepository(context);
        }


        // GET: Group
        public ActionResult Index(int id)
        {
            var group = groupsrepository.getGroupById(id);


            

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
                var user = (from item in context.Users
                            where item.Email == i
                            select item).FirstOrDefault();

                if (user != null)
                {
                    applicationUsers.Add(user);
                }
            }

            applicationUsers.Add((from item in context.Users
                                    where item.Id == loggedInUser
                                    select item).First());


            Group newGroup = new Group { Name = name, OwnerID = loggedInUser, ApplicationUsers = applicationUsers };

            context.Groups.Add(newGroup);

            context.SaveChanges();

            return RedirectToAction("Index", "DashBoard");

        }

        public JsonResult Search(string term)
        {
            var toSearch = new List<string>();

            string userID = User.Identity.GetUserId<string>();
            if (term != null)
            {
                toSearch = (from item in context.Users
                            where item.Id != userID
                            && item.Email.StartsWith(term)
                            select item.Email).ToList();
            }
            else
            {
                toSearch = (from item in context.Users
                            where item.Id != userID
                            select item.Email).ToList();
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
            var group = groupsrepository.getGroupById(groupID);

            var curruser = (from user in context.Users
                            where user.Email == email
                            select user).FirstOrDefault();

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
            var group = groupsrepository.getGroupById(groupID);

            var curruser = (from user in context.Users
                            where user.Email == email
                            select user).FirstOrDefault();

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
            groupsrepository.deleteGroupById(groupID);

            return RedirectToAction("Index", "Dashboard");
        }

    }
}