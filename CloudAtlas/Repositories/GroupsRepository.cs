using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Repositories
{
    public class GroupsRepository
    {
        private readonly ApplicationDbContext db;
        public GroupsRepository(ApplicationDbContext context)
        {
            db = context ?? new ApplicationDbContext();
        }
        public void addGroup(Group group)
        {
            db.Groups.Add(group);
        }
        public Group getGroupById(int id)
        {
            return (from i in db.Groups
                    where i.ID == id
                    select i
                    ).FirstOrDefault();
        }
        public List<Project> getGroupProjects(int id)
        {
            return getGroupById(id).Projects.ToList();
        }
        public void deleteGroupById(int id)
        {
            var group = getGroupById(id);
            db.Groups.Remove(group);
            db.SaveChanges();
        }
        public void addGroupProject(Project project,Group group)
        {
            group.Projects.Add(project);
            db.SaveChanges();
        }
        public void DeleteGroupProject(Project project, Group group)
        {
            group.Projects.Remove(project);
            db.SaveChanges();
        }
        public List<Group> getAllGroupsByUserId(string id)
        {
            List<Group> result = new List<Group>();
            var allGroups = (from i in db.Groups
                             select i);
            foreach(var grou in allGroups)
            {
                foreach(var user in grou.ApplicationUsers)
                {
                    if(user == null)
                    {
                        break;
                    }
                    if(user.Id == id)
                    {
                        result.Add(grou);
                        break;
                    }
                }
            }
            return result;
        }
        
    }
}