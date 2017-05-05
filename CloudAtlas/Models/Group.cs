using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Models
{
    public class Group
    {
        public int ID { get; set; }

        public int ApplicationUserID { get; set; }

        public string Name { get; set; }

<<<<<<< HEAD
     //   public virtual IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
=======
        public virtual ApplicationUser ApplicationUser { get; set; }
>>>>>>> develop

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}