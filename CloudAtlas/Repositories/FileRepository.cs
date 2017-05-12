using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Repositories
{
    public class FileRepository
    {
        private readonly IAppDataContext db;
        public FileRepository(IAppDataContext context)
        {
            db = context ?? new ApplicationDbContext();
        }
        public File getFileById(int id)
        {
            return (from i in db.Files
                    where i.ID == id
                    select i).FirstOrDefault();
        }
        public void addFile(File file)
        {
            db.Files.Add(file);
            db.SaveChanges();
        }
        public void deleteFile(File file)
        {
            db.Files.Remove(file);
            db.SaveChanges();
        }
    }
}