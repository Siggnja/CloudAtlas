﻿using CloudAtlas.Models;
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
            /*
            List<Project> result = new List<Project>();
            var allGroups = (from i in db.Projects
                             select i);
            return new List<Project>();
            */

            var projects = (from item in db.Users
                           where item.Id == id
                           select item.Projects).FirstOrDefault();

            if(projects != null)
            {
                return projects.ToList();
            }
            else
            {
                return new List<Project>();
            }

            /*
            foreach (var proj in allGroups)
            {
                foreach (var user in proj.ApplicationUsers)
                {
                    if (user == null)
                    {
                        break;
                    }
                    if (user.Id == id)
                    {
                        result.Add(proj);
                        break;
                    }
                }
            }
            return result;
            */
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