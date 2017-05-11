using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudAtlas.Models
{
    public class File
    {
        public int ID { get; set; }

        public int FolderID { get; set; }
        [Required]
        //[StringLength(30, ErrorMessage = "File name can not be longer then 30 characters")]
        public string Name { get; set; }
        [Required]
        public string Extension { get; set; }
        //[Required]
        public string Type { get; set; }
        [AllowHtml]
        public string Content { get; set; }





    }
}