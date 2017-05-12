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
                new Folder() { Name = "Folder1", IsRoot = true,SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder2", IsRoot = true, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder3", IsRoot = true, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder4", IsRoot = true, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder5", IsRoot = true, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder6", IsRoot = true, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder7", IsRoot = true, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder8", IsRoot = false, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder9", IsRoot = false, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder10",IsRoot = false, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder11",IsRoot = true, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder12",IsRoot = true, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder13",IsRoot = true, SubFolders = new List<Folder>() },
                new Folder() { Name = "Folder14",IsRoot = true, SubFolders = new List<Folder>() }
            );
         
            context.SaveChanges();
            var id1 = (from i in context.Folders
                       where i.Name == "Folder1"
                       select i).FirstOrDefault();
            var id2 = (from i in context.Folders
                       where i.Name == "Folder2"
                       select i).FirstOrDefault();
            var id3 = (from i in context.Folders
                       where i.Name == "Folder3"
                       select i).FirstOrDefault();
            var id4 = (from i in context.Folders
                       where i.Name == "Folder4"
                       select i).FirstOrDefault();
            var id5 = (from i in context.Folders
                       where i.Name == "Folder5"
                       select i).FirstOrDefault();
            var id6 = (from i in context.Folders
                       where i.Name == "Folder6"
                       select i).FirstOrDefault();
            var id7 = (from i in context.Folders
                       where i.Name == "Folder7"
                       select i).FirstOrDefault();
            var id8 = (from i in context.Folders
                       where i.Name == "Folder8"
                       select i).FirstOrDefault();
            var id9 = (from i in context.Folders
                       where i.Name == "Folder9"
                       select i).FirstOrDefault();
            var id10 = (from i in context.Folders
                       where i.Name == "Folder10"
                       select i).FirstOrDefault();
            var id11 = (from i in context.Folders
                       where i.Name == "Folder11"
                       select i).FirstOrDefault();
            var id12 = (from i in context.Folders
                       where i.Name == "Folder12"
                       select i).FirstOrDefault();
            var id13 = (from i in context.Folders
                       where i.Name == "Folder13"
                       select i).FirstOrDefault();
            var id14 = (from i in context.Folders
                       where i.Name == "Folder14"
                       select i).FirstOrDefault();
            id1.SubFolders.Add(id8);
            id1.SubFolders.Add(id9);
            id9.SubFolders.Add(id10);
            context.Files.AddOrUpdate(x => x.Name,
            new File() { FolderID = id1.ID, Name = "File1", Extension = ".js", Content = "var x = 1;", Type = "Javascript"},
            new File() { FolderID = id1.ID, Name = "File2", Extension = ".js", Content = "var x = 2;", Type = "Javascript"},
            new File() { FolderID = id9.ID, Name = "File3", Extension = ".js", Content = "var x = 3;", Type = "Javascript"},
            new File() { FolderID = id10.ID, Name = "File4", Extension= ".cpp", Content = "var x = 4;",Type = "c_cpp" },
            new File() { FolderID = id3.ID, Name = "File5", Extension = ".cs", Content = "var x = 5;", Type = "csharp"},
            new File() { FolderID = id4.ID, Name = "Pile6", Extension = ".js", Content = "var x = 6;", Type = "Javascript"},
            new File() { FolderID = id5.ID, Name = "File7", Extension = ".js", Content = "var x = 7;", Type = "Javascript" }
          );
            ApplicationUser Kolli = (from i in context.Users
                                     where i.Id == "00537b30-1295-4e84-84b5-db7e76feee70"
                                     select i).FirstOrDefault();
            ApplicationUser Krissi = (from i in context.Users
                                     where i.Id == "853d6b07-0d21-436d-b3df-765380811b3b"
                                     select i).FirstOrDefault();
            ApplicationUser Siggi = (from i in context.Users
                                     where i.Id == "ababc68a-4cea-47dc-9601-d5672f9983fe"
                                     select i).FirstOrDefault();
            ApplicationUser Svessi = (from i in context.Users
                                     where i.Id == "f380908a-8f2e-4441-ae2f-b2a07f8b0e36"
                                     select i).FirstOrDefault();
            List<ApplicationUser> lis1 = new List<ApplicationUser>();
            var group1 = new Group() { Name = "WolfPack", OwnerID = "00537b30-1295-4e84-84b5-db7e76feee70" };
            var group2 = new Group() { Name = "Arsenal", OwnerID = "853d6b07-0d21-436d-b3df-765380811b3b" };
            var group3 = new Group() { Name = "GitKraken", OwnerID = "853d6b07-0d21-436d-b3df-765380811b3b" };
            Kolli.Groups.Add(group1);
            Kolli.Groups.Add(group2);
            Krissi.Groups.Add(group2);
            Svessi.Groups.Add(group3);
            Siggi.Groups.Add(group2);
            context.SaveChanges();
            var proj1 = new Project() { Name = "Project1", Type = "Javascript", IsGroupProject = true,  FolderID = id1.ID };
            var proj2 = new Project() { Name = "Project2", Type = "Javascript", IsGroupProject = false, FolderID = id2.ID };
            var proj3 = new Project() { Name = "Project3", Type = "Javascript", IsGroupProject = false, FolderID = id3.ID };
            var proj4 = new Project() { Name = "Project4", Type = "Javascript", IsGroupProject = false, FolderID = id4.ID };
            var proj5 = new Project() { Name = "Project5", Type = "Javascript", IsGroupProject = false, FolderID = id5.ID };
            var proj6 = new Project() { Name = "Project6", Type = "Javascript", IsGroupProject = false, FolderID = id6.ID };
            var proj7 = new Project() { Name = "Project7", Type = "Javascript", IsGroupProject = true,  FolderID = id7.ID };
           
            Krissi.Projects.Add(proj1);
            Krissi.Projects.Add(proj2);
            Krissi.Projects.Add(proj3);
            Kolli.Projects.Add(proj4);
            Kolli.Projects.Add(proj5);
            Kolli.Projects.Add(proj7);
            Kolli.Projects.Add(proj6);
            Kolli.Projects.Add(proj7);
            Svessi.Projects.Add(proj1);
            Svessi.Projects.Add(proj2);
            Svessi.Projects.Add(proj3);
            Siggi.Projects.Add(proj1);
            Siggi.Projects.Add(proj2);
            Siggi.Projects.Add(proj3);
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            */
        }
    }
}