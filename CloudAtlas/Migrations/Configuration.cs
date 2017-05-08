namespace CloudAtlas.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CloudAtlas.Models;

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
           

            context.Folders.AddOrUpdate(x => x.Name,
                new Folder() { Name=  "Folder8",  IsRoot=true},
                new Folder() { Name = "Folder9", IsRoot = true},
                new Folder() { Name = "Folder10", IsRoot = true},
                new Folder() { Name = "Folder11", IsRoot = true},
                new Folder() { Name = "Folder12", IsRoot = true},
                new Folder() { Name = "Folder13", IsRoot = true},
                new Folder() { Name = "Folder14", IsRoot = true}

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
                new File() { ID = 1, FolderID = id1, Name = "File11", Extension = ".js", Content = "var x = 1;" },
                new File() { ID = 2, FolderID = id2, Name = "File21", Extension = ".js", Content = "var x = 2;" },
                new File() { ID = 3, FolderID = id3, Name = "File31", Extension = ".js", Content = "var x = 3;" },
                new File() { ID = 4, FolderID = id4, Name = "File41", Extension = ".js", Content = "var x = 4;" },
                new File() { ID = 5, FolderID = id5, Name = "File51", Extension = ".js", Content = "var x = 5;" },
                new File() { ID = 6, FolderID = id6, Name = "Pile61", Extension = ".js", Content = "var x = 6;" },
                new File() { ID = 7, FolderID = id7, Name = "File71", Extension = ".js", Content = "var x = 7;" }
            );
            context.Projects.AddOrUpdate(x => x.Name,
                new Project() {Name=  "Project1",Type="Javascript",IsGroupProject=true, FolderID=id1,ApplicationUserID= "0272c0a9-94eb-4fdc-bca0-798d4bb48db4"},
                new Project() {Name = "Project2", Type = "Javascript", IsGroupProject = false, FolderID = id2, ApplicationUserID = "0272c0a9-94eb-4fdc-bca0-798d4bb48db4" },
                new Project() {Name = "Project3", Type = "Javascript", IsGroupProject = false, FolderID = id3, ApplicationUserID = "0272c0a9-94eb-4fdc-bca0-798d4bb48db4" },
                new Project() {Name = "Project4", Type = "Javascript", IsGroupProject = false, FolderID = id4, ApplicationUserID = "ac54b68f-cf66-42e6-8688-571f11a65be1" },
                new Project() {Name = "Project5", Type = "Javascript", IsGroupProject = false, FolderID = id5, ApplicationUserID = "ac54b68f-cf66-42e6-8688-571f11a65be1" },
                new Project() {Name = "Project6", Type = "Javascript", IsGroupProject = false, FolderID = id6, ApplicationUserID = "b117e192-a2bb-4df9-b740-9c9d7323ba6d" },
                new Project() {Name = "Project7", Type = "Javascript", IsGroupProject = true,FolderID = id7, ApplicationUserID = "b117e192-a2bb-4df9-b740-9c9d7323ba6d" }
            );

            context.Groups.AddOrUpdate(x => x.Name,
                new Group() {Name="WolfPack",OwnerID = "0272c0a9-94eb-4fdc-bca0-798d4bb48db4"},
                new Group() {Name = "Arsenal", OwnerID = "b117e192-a2bb-4df9-b740-9c9d7323ba6d" }
            );
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
        }
    }
}
