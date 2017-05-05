using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Models
{
    public class DashboardViewModel
    {
        public List<Project> Projects { get; set; }
        public List<Group> Groups { get; set; }
    }
}