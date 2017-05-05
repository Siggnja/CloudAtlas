using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public int ApplicationUserID { get; set; }

        public string StartupFile { get; set; }

        public bool IsGroupProject { get; set; }

        public int FolderID { get; set; }

        public virtual Folder Folder { get; set; }

       // public virtual ApplicationUser ApplicationUser { get; set; }
    }
}