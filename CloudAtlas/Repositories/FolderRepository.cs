using CloudAtlas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudAtlas.Repositories
{
    public class FolderRepository : BaseRepository
    {
        public FolderRepository(IAppDataContext context) : base(context)
        {
        }
        public Folder GetParentFolder(Folder folder)
        {
            return folder.Parent;
        }
        public List<File> GetAllFilesById(int id)
        {
            var fold = GetFolderByID(id);
            return fold.Files.ToList();
        }
        public List<Folder> GetFoldersFromRootID(int id)
        {
            return (from fold in db.Folders
                    where fold.ParentID == id
                    select fold).ToList();
        }
        public List<Folder> GetSubFolders(Folder folder)
        {
            return folder.SubFolders.ToList();
        }
        public void UpdateFolderNameByID(int id, string name)
        {
            var folder = GetFolderByID((int)id);
            folder.Name = name.Trim();
            db.SaveChanges();
        }
        public void RemoveFolderByID(int id)
        {
            Folder deleteMe = GetFolderByID(id);
            Folder parent = deleteMe.Parent;

            if (parent != null)
            {
                parent.SubFolders.Remove(deleteMe);
            }
            DeleteFolderHelper(deleteMe);


            db.Folders.Remove(deleteMe);

            db.SaveChanges();
        }
        public void AddFolder(Folder folder, Folder par)
        {
            db.Folders.Add(folder);
            par.SubFolders.Add(folder);
            db.SaveChanges();
        }
        public void AddFolder(Folder folder)
        {
            db.Folders.Add(folder);
            db.SaveChanges();
        }

        private void DeleteFolderHelper(Folder deleteMe)
        {
            if (deleteMe != null && deleteMe.SubFolders != null)
            {
                for (int i = 0; i < deleteMe.SubFolders.Count; i++)
                {
                    DeleteFolderHelper(deleteMe.SubFolders.ElementAt(i));
                    DeleteAllFiles(deleteMe.SubFolders.ElementAt(i));
                    db.Folders.Remove(deleteMe.SubFolders.ElementAt(i));

                }
            }
        }
        public void RemoveFileFromFolder(int id, File file)
        {
            GetFolderByID(id).Files.Remove(file);
        }

        private void DeleteAllFiles(Folder fold)
        {
            if (fold.Files != null)
            {
                for (int i = 0; i < fold.Files.Count; i++)
                {
                    var id = fold.Files.ElementAt(i).ID;

                    var file = GetFileByID(id);
                    RemoveFileFromFolder(file.FolderID, file);
                    db.Files.Remove(file);
                    db.SaveChanges();
                }
            }
        }

        public void AddFileToFolder(int id, File file)
        {
            GetFolderByID(id).Files.Add(file);
        }
    }
}