﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Models
{
    public class Folder
    {
        public int ID { get; set; }

        public int ProjectID { get; set; }
        
        public string Name { get; set; }

        public bool IsRoot { get; set; }
        public virtual ICollection<Folder> SubFolders { get; set; }

        public virtual ICollection<File> Files { get; set; }







    }
}