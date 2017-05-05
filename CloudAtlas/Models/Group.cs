using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Models
{
    public class Group
    {
        public int ID { get; set; }

        public int OwnerID { get; set; }

        public string Name { get; set; }

     //   public virtual IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

        public virtual IEnumerable<Project> Projects { get; set; }

    }
}