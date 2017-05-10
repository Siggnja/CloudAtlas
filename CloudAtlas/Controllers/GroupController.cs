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
    public class GroupController : Controller
    {
        private readonly ApplicationDbContext context;

        public GroupController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Group
        public ActionResult Index()
        {
            return View();
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
    
    }
}