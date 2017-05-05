using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Models
{
    public class File
    {
        public int ID { get; set; }

        public int FolderID { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public string Content { get; set; }



    }
}