using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudAtlas.Models;

namespace CloudAtlas.Tests.TestHelper
{
    public static class InitMockData
    {
        public static void InitData(MockDatabase mockDb)
        { 
            ApplicationUser Siggi = new ApplicationUser() { Email = "siggnja@gmail.com", UserName = "Siggi", Id = "00537b30-1295-4e84-84b5-db7e76feee70", EmailConfirmed = false, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0,Groups = new List<Group>(),Projects = new List<Project>() };
            ApplicationUser Svessi = new ApplicationUser() { Email = "svesig@gmail.com", UserName = "svesig", Id = "f380908a-8f2e-4441-ae2f-b2a07f8b0e36", EmailConfirmed = false, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0, Groups = new List<Group>(), Projects = new List<Project>() };
            ApplicationUser Kolli = new ApplicationUser() { Email = "kolli@gmail.com", UserName = "kolli", Id = "00537b30-1295-4e84-84b5-db7e76feee70", EmailConfirmed = false, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0, Groups = new List<Group>(), Projects = new List<Project>() };

            var fold1 = new Folder() { ID = 1, Name = "Folder1", IsRoot = true, SubFolders = new List<Folder>(), Files = new List<File>() };
            var fold2 = new Folder() { ID = 2, Name = "Folder2", IsRoot = false, SubFolders = new List<Folder>(), Files = new List<File>() };
            var fold3 = new Folder() { ID = 3, Name = "Folder3", IsRoot = true, SubFolders = new List<Folder>(), Files = new List<File>() };
            var fold4 = new Folder() { ID = 4, Name = "Folder4", IsRoot = true, SubFolders = new List<Folder>(), Files = new List<File>() };
            var fold5 = new Folder() { ID = 5, Name = "Folder5", IsRoot = true, SubFolders = new List<Folder>(), Files = new List<File>() };

            var file1 = new File() { ID = 1, FolderID = fold1.ID, Name = "File1", Extension = ".js", Content = "var x = 1;", Type = "Javascript" };
            var file2 = new File() { ID = 2, FolderID = fold2.ID, Name = "File2", Extension = ".js", Content = "var x = 2;", Type = "Javascript" };
            var file3 = new File() { ID = 3, FolderID = fold3.ID, Name = "File3", Extension = ".js", Content = "var x = 3;", Type = "Javascript" };
            var file4 = new File() { ID = 4, FolderID = fold4.ID, Name = "File4", Extension = ".cpp", Content = "var x = 4;", Type = "c_cpp" };
            var file5 = new File() { ID = 5, FolderID = fold5.ID, Name = "File5", Extension = ".cs", Content = "var x = 5;", Type = "csharp" };

            var group1 = new Group() {ID=1, Name = "WolfPack", OwnerID = "00537b30-1295-4e84-84b5-db7e76feee70" };
            var group2 = new Group() {ID=2, Name = "Arsenal", OwnerID = "f380908a-8f2e-4441-ae2f-b2a07f8b0e36" };
            var group3 = new Group() {ID=3, Name = "GitKraken", OwnerID = "00537b30-1295-4e84-84b5-db7e76feee70" };

            var proj1 = new Project() {ID=1, Name = "Project1", Type = "Javascript", IsGroupProject = false, FolderID = fold1.ID };
            var proj2 = new Project() {ID=2, Name = "Project2", Type = "Javascript", IsGroupProject = false, FolderID = fold2.ID };
            var proj3 = new Project() {ID=3, Name = "Project3", Type = "Javascript", IsGroupProject = false, FolderID = fold3.ID };
            var proj4 = new Project() {ID=4, Name = "Project4", Type = "Javascript", IsGroupProject = false, FolderID = fold4.ID };

            fold1.Files.Add(file1);
            fold1.SubFolders.Add(fold5);
            fold2.Files.Add(file2);
            fold3.Files.Add(file3);
            fold4.Files.Add(file4);

            

            Siggi.Groups.Add(group1);
            Siggi.Projects.Add(proj1);
            Siggi.Projects.Add(proj2);
            mockDb.Users.Add(Siggi);
            mockDb.Users.Add(Svessi);
            mockDb.Users.Add(Kolli);

            mockDb.Files.Add(file1);
            mockDb.Files.Add(file2);
            mockDb.Files.Add(file3);
            mockDb.Files.Add(file4);
            mockDb.Files.Add(file5);

            mockDb.Folders.Add(fold1);
            mockDb.Folders.Add(fold2);
            mockDb.Folders.Add(fold3);
            mockDb.Folders.Add(fold4);
            mockDb.Folders.Add(fold5);

            mockDb.Groups.Add(group1);
            mockDb.Groups.Add(group2);
            mockDb.Groups.Add(group3);


            mockDb.Projects.Add(proj1);
            mockDb.Projects.Add(proj2);
            mockDb.Projects.Add(proj3);
            mockDb.Projects.Add(proj4);
        }
    }
}
