using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Repositories
{
    public class BaseRepository
    {
        public readonly IAppDataContext db;

        public BaseRepository(IAppDataContext context)
        {
            db = context ?? new ApplicationDbContext();
        }
        public ApplicationUser GetUserByID(string ID)
        {
            var toReturn = (from item in db.Users
                            where item.Id == ID
                            select item).FirstOrDefault();

            return toReturn;
        }

        public List<Group> GetGroupsByUserID(string id)
        {
            return (from item in db.Users
                    where item.Id == id
                    select item.Groups).FirstOrDefault().ToList();
        }

        public List<string> SearchUserByEmail(string email, string id)
        {
            return (from user in db.Users
                    where user.Id != id
                    && user.Email.ToLower().StartsWith(email.ToLower())
                    select user.Email).ToList();
        }

        public List<string> SearchGroupByName(string loggedInUser, string name)
        {
            var userGroup = (from user in db.Users
                             where user.Id == loggedInUser
                             select user.Groups).FirstOrDefault();

            return (from gr in userGroup
                    where gr.Name.ToLower().StartsWith(name.ToLower())
                    select gr.Name).ToList();
        }

        public List<ApplicationUser> GetUsers()
        {
            return (from item in db.Users
                    select item).ToList();

        }
        public Folder GetFolderByID(int id)
        {
            return (from fold in db.Folders
                    where fold.ID == id
                    select fold).FirstOrDefault();
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return (from item in db.Users
                    where item.Email == email
                    select item).FirstOrDefault();
        }

        public File GetFileByID(int id)
        {
            return (from file in db.Files
                    where file.ID == id
                    select file).FirstOrDefault();
        }
        public Group GetGroupById(int id)
        {
            return (from i in db.Groups
                    where i.ID == id
                    select i
                    ).FirstOrDefault();
        }
    }
}