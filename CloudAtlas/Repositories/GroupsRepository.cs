using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Repositories
{
    public class GroupsRepository
    {
        private readonly IAppDataContext db;
        public GroupsRepository(IAppDataContext context)
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

        public void deleteGroupById(int id)
        {
            var group = getGroupById(id);
            db.Groups.Remove(group);
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
            return (from item in db.Users
                    where item.Id == id
                    select item.Groups).FirstOrDefault().ToList();
        }
        
    }
}