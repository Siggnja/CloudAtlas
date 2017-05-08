using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Repositories
{
    public class FolderRepository
    {
        private readonly ApplicationDbContext db;
        public FolderRepository(ApplicationDbContext context)
        {
            db = context ?? new ApplicationDbContext();
        }
        public Folder GetFolderByID(int id)
        {
            return (from i in db.Folders
                    where i.ID == id
                    select i).FirstOrDefault();
        }
        public Folder GetParentFolder(Folder folder)
        {
            return folder.Parent;               
        }
        public List<Folder> GetSubFolders(Folder folder)
        {
            return folder.SubFolders.ToList();
        }
        public void addFolder(Folder folder)
        {
            db.Folders.Add(folder);
        }
        public void removeFolder(Folder folder)
        {
            db.Folders.Remove(folder);
        }
        public List<File> getAllFilesById(int id)
        {
            var fold = GetFolderByID(id);
            return fold.Files.ToList();
        }
        public void addFileToFolder(int id,File file)
        {
            GetFolderByID(id).Files.Add(file);
        }
        public void removeFileFromFolder(int id, File file)
        {
            GetFolderByID(id).Files.Remove(file);
        }
        /*
      /*
    public Project GetProjectById(int id)
    {
        return (from i in db.Projects
                where i.ID == id
                select i).FirstOrDefault();
    }
    public void AddProject(Project project)
    {
        db.Projects.Add(project);
        db.SaveChanges();
    }
    public void RemoveProject(Project project)
    {
        db.Projects.Remove(project);
        db.SaveChanges();
    }
    public void UpdateProject(EntityState state, Project project)
    {
        db.Entry(project).State = state;
    }
    */
    }
}