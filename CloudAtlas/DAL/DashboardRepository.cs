using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.DAL
{
    public class DashboardRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public List<Project> GetProjects(string Id)
        {
            var p = (from item in context.Projects
                     where item.ApplicationUserID == Id
                     select item).ToList();
            return p;
        }
    }
}