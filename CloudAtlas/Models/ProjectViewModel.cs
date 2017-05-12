using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Models
{
    public class ProjectViewModel
    {
        public Folder Root { get; set; }

        public Project Project { get; set; }

        public ApplicationUser User { get; set; }

        public List<Folder> Folders { get; set; }

        public List<File> Files { get; set; }


    }
}