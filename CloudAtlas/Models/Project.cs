using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudAtlas.Models
{
    public class Project
    {
        public int ID { get; set; }
        [Required]
        //[StringLength(30,ErrorMessage = "Name can not be longer then 30 characters")]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }


        public int FolderID { get; set; }

        private DateTime _date = DateTime.Now;

        public DateTime DateCreated
        {
            get { return _date; }
            set { _date = value;}
        }
        public virtual Folder Folder { get; set; }

        public string OwnerID { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}