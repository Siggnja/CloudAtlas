namespace CloudAtlas.Migrations
{
    using CloudAtlas.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CloudAtlas.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CloudAtlas.Models.ApplicationDbContext";
        }

        protected override void Seed(CloudAtlas.Models.ApplicationDbContext context)
        {
            /*

            context.Folders.AddOrUpdate(x => x.Name,
                new Folder() { Name = "Folder8", IsRoot = true },
                new Folder() { Name = "Folder9", IsRoot = true },
                new Folder() { Name = "Folder10", IsRoot = true },
                new Folder() { Name = "Folder11", IsRoot = true },
                new Folder() { Name = "Folder12", IsRoot = true },
                new Folder() { Name = "Folder13", IsRoot = true },
                new Folder() { Name = "Folder14", IsRoot = true }

            );
            context.SaveChanges();
            var id1 = (from i in context.Folders
                       where i.Name == "Folder8"
                       select i.ID).FirstOrDefault();
            var id2 = (from i in context.Folders
                       where i.Name == "Folder9"
                       select i.ID).FirstOrDefault();
            var id3 = (from i in context.Folders
                       where i.Name == "Folder10"
                       select i.ID).FirstOrDefault();
            var id4 = (from i in context.Folders
                       where i.Name == "Folder11"
                       select i.ID).FirstOrDefault();
            var id5 = (from i in context.Folders
                       where i.Name == "Folder12"
                       select i.ID).FirstOrDefault();
            var id6 = (from i in context.Folders
                       where i.Name == "Folder13"
                       select i.ID).FirstOrDefault();
            var id7 = (from i in context.Folders
                       where i.Name == "Folder14"
                       select i.ID).FirstOrDefault();

            context.Files.AddOrUpdate(x => x.Name,
                new File() { FolderID = id1, Name = "File1", Extension = ".js", Content = "var x = 1;" },
                new File() { FolderID = id2, Name = "File2", Extension = ".js", Content = "var x = 2;" },
                new File() { FolderID = id3, Name = "File3", Extension = ".js", Content = "var x = 3;" },
                new File() { FolderID = id4, Name = "File4", Extension = ".js", Content = "var x = 4;" },
                new File() { FolderID = id5, Name = "File5", Extension = ".js", Content = "var x = 5;" },
                new File() { FolderID = id6, Name = "Pile6", Extension = ".js", Content = "var x = 6;" },
                new File() { FolderID = id7, Name = "File7", Extension = ".js", Content = "var x = 7;" }
            );
            ApplicationUser user1 = (from i in context.Users
                                     where i.Id == "0272c0a9-94eb-4fdc-bca0-798d4bb48db4"
                                     select i).FirstOrDefault();
            ApplicationUser user2 = (from i in context.Users
                                     where i.Id == "b117e192-a2bb-4df9-b740-9c9d7323ba6d"
                                     select i).FirstOrDefault();
            ApplicationUser user3 = (from i in context.Users
                                     where i.Id == "ac54b68f-cf66-42e6-8688-571f11a65be1"
                                     select i).FirstOrDefault();
            List<ApplicationUser> lis1 = new List<ApplicationUser>();
            var group1 = new Group() { Name = "WolfPack", OwnerID = "0272c0a9-94eb-4fdc-bca0-798d4bb48db4" };
            var group2 = new Group() { Name = "Arsenal", OwnerID = "b117e192-a2bb-4df9-b740-9c9d7323ba6d" };
            user1.Groups.Add(group1);
            user1.Groups.Add(group2);
            user2.Groups.Add(group1);
            user2.Groups.Add(group2);

            context.SaveChanges();

            var proj1 = new Project() { Name = "Project1", Type = "Javascript", IsGroupProject = true, FolderID = id1 };
            var proj2 = new Project() { Name = "Project2", Type = "Javascript", IsGroupProject = false, FolderID = id2 };
            var proj3 = new Project() { Name = "Project3", Type = "Javascript", IsGroupProject = false, FolderID = id3 };
            var proj4 = new Project() { Name = "Project4", Type = "Javascript", IsGroupProject = false, FolderID = id4 };
            var proj5 = new Project() { Name = "Project5", Type = "Javascript", IsGroupProject = false, FolderID = id5 };
            var proj6 = new Project() { Name = "Project6", Type = "Javascript", IsGroupProject = false, FolderID = id6 };
            var proj7 = new Project() { Name = "Project7", Type = "Javascript", IsGroupProject = true, FolderID = id7 };

            user1.Projects.Add(proj1);
            user1.Projects.Add(proj2);
            user1.Projects.Add(proj3);
            user2.Projects.Add(proj4);
            user2.Projects.Add(proj5);
            user2.Projects.Add(proj7);
            user3.Projects.Add(proj6);
            user3.Projects.Add(proj7);


            context.SaveChanges();
            */
        }
    }
}
