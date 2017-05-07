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


            context.Folders.AddOrUpdate(x => x.ID,
                new Folder() { ID = 1, Name="Folder1",IsRoot=true},
                new Folder() { ID = 2, Name = "Folder2", IsRoot = true},
                new Folder() { ID = 3, Name = "Folder3", IsRoot = true},
                new Folder() { ID = 4, Name = "Folder4", IsRoot = true},
                new Folder() { ID = 5, Name = "Folder5", IsRoot = true},
                new Folder() { ID = 6, Name = "Folder6", IsRoot = true},
                new Folder() { ID = 7, Name = "Folder7", IsRoot = true}

            );
            context.Files.AddOrUpdate(x => x.ID,
                new File() { ID = 1, FolderID = 1, Name = "File1", Extension = ".js", Content = "var x = 1;" },
                new File() { ID = 2, FolderID = 2, Name = "File2", Extension = ".js", Content = "var x = 2;" },
                new File() { ID = 3, FolderID = 3, Name = "File3", Extension = ".js", Content = "var x = 3;" },
                new File() { ID = 4, FolderID = 4, Name = "File4", Extension = ".js", Content = "var x = 4;" },
                new File() { ID = 5, FolderID = 5, Name = "File5", Extension = ".js", Content = "var x = 5;" },
                new File() { ID = 6, FolderID = 6, Name = "Pile6", Extension = ".js", Content = "var x = 6;" },
                new File() { ID = 7, FolderID = 7, Name = "File7", Extension = ".js", Content = "var x = 7;" }
            );
            context.Projects.AddOrUpdate(x => x.Name,
                new Project() {Name="Project1",Type="Javascript",IsGroupProject=true,FolderID=1,ApplicationUserID= "0272c0a9-94eb-4fdc-bca0-798d4bb48db4"},
                new Project() {Name = "Project2", Type = "Javascript", IsGroupProject = false, FolderID = 2, ApplicationUserID = "0272c0a9-94eb-4fdc-bca0-798d4bb48db4" },
                new Project() {Name = "Project3", Type = "Javascript", IsGroupProject = false, FolderID = 3, ApplicationUserID = "0272c0a9-94eb-4fdc-bca0-798d4bb48db4" },
                new Project() {Name = "Project4", Type = "Javascript", IsGroupProject = false, FolderID = 4, ApplicationUserID = "ac54b68f-cf66-42e6-8688-571f11a65be1" },
                new Project() {Name = "Project5", Type = "Javascript", IsGroupProject = false, FolderID = 5, ApplicationUserID = "ac54b68f-cf66-42e6-8688-571f11a65be1" },
                new Project() {Name = "Project6", Type = "Javascript", IsGroupProject = false, FolderID = 6, ApplicationUserID = "b117e192-a2bb-4df9-b740-9c9d7323ba6d" },
                new Project() {Name = "Project7", Type = "Javascript", IsGroupProject = true,FolderID = 7, ApplicationUserID = "b117e192-a2bb-4df9-b740-9c9d7323ba6d" }
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
