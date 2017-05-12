using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAtlas.Tests
{
    public class MockDatabase : IAppDataContext
    {
        public MockDatabase()
        {
            // We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
            Projects = new InMemoryDbSet<Project>();
            Folders = new InMemoryDbSet<Folder>();
            Files = new InMemoryDbSet<File>();
            Groups = new InMemoryDbSet<Group>();
            Users = new InMemoryDbSet<ApplicationUser>();
        }

        public IDbSet<Project> Projects { get; set; }

        public IDbSet<Folder> Folders { get; set; }

        public IDbSet<File> Files { get; set; }

        public IDbSet<Group> Groups { get; set; }
        public IDbSet<ApplicationUser> Users { get; set; }

        public int SaveChanges()
        {
            // Pretend that each entity gets a database id when we hit save.
            int changes = 0;

            return changes;
        }

        public void Dispose()
        {
            // Do nothing!
        }
    }

}
