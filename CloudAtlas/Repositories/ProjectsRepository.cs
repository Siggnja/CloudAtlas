using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CloudAtlas.Repositories
{
    public class ProjectsRepository : BaseRepository
    {
        public ProjectsRepository(IAppDataContext context) : base(context)
        {
        }

        public void AddProjectToUser(Project proj, ApplicationUser user)
        {
            user.Projects.Add(proj);
            db.SaveChanges();
        }
        public void DeleteFromProject(Project proj, ApplicationUser user)
        {
            user.Projects.Remove(proj);
            db.SaveChanges();
        }
        public List<Project> GetProjectsByUserId(string id)
        {
            return (from item in db.Users
                    where item.Id == id
                    select item.Projects).FirstOrDefault().ToList();

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
    }
}