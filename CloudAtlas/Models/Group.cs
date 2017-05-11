using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudAtlas.Models
{
    public class Group
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name can not be empty")]
        public string Name { get; set; }

        public string OwnerID { get; set; }

        public virtual ApplicationUser Owner {get; set;}
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}