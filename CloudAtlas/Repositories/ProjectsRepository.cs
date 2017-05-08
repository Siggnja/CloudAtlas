using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CloudAtlas.Repositories
{
    public class ProjectsRepository
    {
        private readonly ApplicationDbContext db;
        public ProjectsRepository(ApplicationDbContext context)
        {
            db = context ?? new ApplicationDbContext();
        }
        public List<Project> GetProjectByUserId(string id)
        {

            return (from i in db.Projects
                        where i.ApplicationUserID == id
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
        public void UpdateProject(EntityState state,Project project)
        {
            db.Entry(project).State = state;
        }
    }
}