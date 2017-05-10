using CloudAtlas.Models;
using CloudAtlas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudAtlas.Controllers
{
    public class GroupController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        private readonly GroupsRepository groupsrepository;

        public GroupController()
        {
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
            return View();
        }

        
    }
}