using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CloudAtlas.Models
{
    public class Folder
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public bool IsRoot { get; set; }

        public int? ParentID { get; set; }

        public int? FolderID { get; set; }

        [InverseProperty("SubFolders")]
        public virtual Folder Parent { get; set; }
        [InverseProperty("Parent")]
        public virtual ICollection<Folder> SubFolders { get; set; }

        public virtual ICollection<File> Files { get; set; }





    }
}