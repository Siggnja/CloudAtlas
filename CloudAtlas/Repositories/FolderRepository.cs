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
        /*
        public List<Project> GetParentFolder(string id)
        {
            return (from i in db.Folders
                    where i.
                    select i).ToList();
               
        }

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