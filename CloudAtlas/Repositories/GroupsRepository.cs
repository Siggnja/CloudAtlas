using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Repositories
{
    public class GroupsRepository : BaseRepository
    {
        public void AddGroup(Group group)
        {
            db.Groups.Add(group);
            db.SaveChanges();
        }
        public List<Project> getGroupProjects(int id)
        {
            return GetGroupById(id).Projects.ToList();
        }
        public void DeleteGroupById(int id)
        {
            var group = GetGroupById(id);
            db.Groups.Remove(group);
            db.SaveChanges();
        }
        public void AddGroupProject(Project project,Group group)
        {
            group.Projects.Add(project);
            db.SaveChanges();
        }
        public void DeleteGroupProject(Project project, Group group)
        {
            group.Projects.Remove(project);
            db.SaveChanges();
        }
        public void AddUserToGroup(ApplicationUser user, Group group)
        {
            group.ApplicationUsers.Add(user);
            db.SaveChanges();
        }
        public void RemoveUserFromGroup(ApplicationUser user, Group group)
        {
            group.ApplicationUsers.Remove(user);
            db.SaveChanges();
        }
        public bool UserInGroup(ApplicationUser user, Group group)
        {
            return group.ApplicationUsers.Contains(user);
        }

        public List<Group> getAllGroupsByUserId(string id)
        {
            /*
            List<Group> result = new List<Group>();
            var allGroups = (from i in db.Groups
                             select i);
*/
            return (from item in db.Users
                    where item.Id == id
                    select item.Groups).FirstOrDefault().ToList();
            /*
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
            */
        }
        
    }
}