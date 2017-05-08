using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Repositories
{
    public class FileRepository
    {
        private readonly ApplicationDbContext db;
        public FileRepository(ApplicationDbContext context)
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
        }
        public void deleteFile(File file)
        {
            db.Files.Remove(file);
        }
    }
}