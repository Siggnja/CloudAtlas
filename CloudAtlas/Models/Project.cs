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
        public IEnumerable<Folder> Folders { get; set; }
    }
}