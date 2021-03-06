﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CloudAtlas.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    { 

        public virtual ICollection<Project> Projects { get; set; }
        [InverseProperty("ApplicationUsers")]
        public virtual ICollection<Group> Groups { get; set; }
        [InverseProperty("Owner")]
        public virtual ICollection<Group> OwnedGroups { get; set; }

        public string AvatarPath { get; set; }

        public string Theme { get; set; }
        
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        { 
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public interface IAppDataContext
    {
        IDbSet<Project> Projects { get; set; }
        IDbSet<Folder> Folders { get; set; }

        IDbSet<File> Files { get; set; }

        IDbSet<Group> Groups { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }

        int SaveChanges();
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDataContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Project> Projects { get; set; }

        public IDbSet<Folder> Folders { get; set; }

        public IDbSet<File> Files { get; set; }

        public IDbSet<Group> Groups { get; set; }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());

            builder.Conventions.Remove<PluralizingTableNameConvention>();
            builder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            builder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            builder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}