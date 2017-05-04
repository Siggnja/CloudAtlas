using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string EditorTheme { get; set; }

        public virtual IEnumerable<User> Groups { get; set; }

        public virtual IEnumerable<Project> Projects { get; set; }


    }
}