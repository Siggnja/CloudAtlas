using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Repositories
{
    public class FileRepository : BaseRepository
    {
        public File GetFileById(int id)
        {
            return (from i in db.Files
                    where i.ID == id
                    select i).FirstOrDefault();
        }
        public void UpdateFileNameByID(int id, string name)
        {
            var file = GetFileByID((int)id);
            file.Name = name.Trim();
            db.SaveChanges();
        }
        public void SaveFile(int id, string content)
        {
            var thisfile = GetFileByID(id);

            thisfile.Content = content;

            db.SaveChanges();
        }
        public void AddFile(File file)
        {
            db.Files.Add(file);
            db.SaveChanges();
        }
        public List<File> GetFilesByFolderID(int id)
        {
            return (from file in db.Files
                    where file.FolderID == id
                    select file).ToList();
        }
        public void RemoveFileByID(int id)
        {
            var file = GetFileByID(id);
            RemoveFileFromFolder(file.FolderID, file);
            db.Files.Remove(file);
            db.SaveChanges();
        }

        public void RemoveFileFromFolder(int id, File file)
        {
            GetFolderByID(id).Files.Remove(file);
        }

    }
}