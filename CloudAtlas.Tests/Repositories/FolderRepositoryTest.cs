using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudAtlas.Repositories;
using CloudAtlas.Models;
using System.Collections.Generic;
using System.Linq;
using CloudAtlas.Tests.TestHelper;

namespace CloudAtlas.Tests.Repositories
{
    [TestClass]
    public class FolderRepositoryTest
    {
        private FolderRepository _folder;

        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDatabase();
            InitMockData.InitData(mockDb);
            _folder = new FolderRepository(mockDb);
        }

        [TestMethod]
        public void TestGetFolderByID()
        {
            //Arrange
            var getFolder = _folder.GetFolderByID(1);
            //Assert
            Assert.IsNotNull(getFolder);

        }
        [TestMethod]
        public void TestAddFolder()
        {
            //Arrange
            var newFolder = new Folder() { ID = 6, Name = "Folder1", IsRoot = true, SubFolders = new List<Folder>(), Files = new List<File>() };
            _folder.AddFolder(newFolder);
            //Act
            var getFolder = _folder.GetFolderByID(newFolder.ID);
            //Assert
            Assert.IsNotNull(getFolder);

        }
        [TestMethod]
        public void TestGetParentFolder()
        {
            //Arrange
            var parent = new Folder() { ID = 6, Name = "Folder6", IsRoot = true, SubFolders = new List<Folder>(), Files = new List<File>() };
            var child = new Folder() { ID = 7, Name = "Folder7", ParentID =6 ,Parent=parent,IsRoot = false, SubFolders = new List<Folder>(), Files = new List<File>() };

            //Act
            parent.SubFolders.Add(child);
            _folder.AddFolder(parent);
            _folder.AddFolder(child);
            //Assert
            Assert.AreEqual(parent,_folder.GetParentFolder(child));

        }
        [TestMethod]
        public void TestGetSubFolder()
        {
            //Arrange
            var parent = new Folder() { ID = 6, Name = "Folder6", IsRoot = true, SubFolders = new List<Folder>(), Files = new List<File>() };
            var child1 = new Folder() { ID = 7, Name = "Folder7", ParentID = 6, Parent = parent, IsRoot = false, SubFolders = new List<Folder>(), Files = new List<File>() };
            var child2 = new Folder() { ID = 8, Name = "Folder8", ParentID = 6, Parent = parent, IsRoot = false, SubFolders = new List<Folder>(), Files = new List<File>() };
            //Act
            parent.SubFolders.Add(child1);
            parent.SubFolders.Add(child2);
            _folder.AddFolder(parent);
            _folder.AddFolder(child1);
            _folder.AddFolder(child2);
            

            //Convert both to list
            List<Folder> res1 = parent.SubFolders.ToList();
            List<Folder> res2 = _folder.GetSubFolders(parent);
            //Assert - Use IsTrue because of object refrences
            Assert.AreEqual(res1.Count,res2.Count);

        }
        [TestMethod]
        public void TestRemoveFolder()
        {
            //Arrange
            var newFolder = new Folder() { ID = 6, Name = "Folder6", IsRoot = false, SubFolders = new List<Folder>(), Files = new List<File>() };
            _folder.AddFolder(newFolder);
            //Act
            _folder.RemoveFolderByID(newFolder.ID);
            var getFolder = _folder.GetFolderByID(newFolder.ID);

            //Assert
            Assert.IsNull(getFolder);

        }
        [TestMethod]
        public void TestGetAllFiles()
        {
            var res = _folder.GetAllFilesById(1);
            //Assert
            Assert.AreEqual(res.ElementAt(0).ID, 1);
        }
        [TestMethod]
        public void TestAddFileToFolder()
        {
            Folder fold5 = _folder.GetFolderByID(5);
            var newFile = new File
            {
                ID = 2,
                Content = "var x = 0;",
                Name = "init.js",
                Extension = ".js",
                Type = "javascript",
                FolderID = 2
            };
            _folder.AddFileToFolder(5,newFile);
            //Act
            List<File> allFiles = _folder.GetAllFilesById(fold5.ID);
            //Assert
            Assert.AreEqual(allFiles.Count,1);
            Assert.AreEqual(allFiles.ElementAt(0).ID, newFile.ID);
        }
        [TestMethod]
        public void TestRemoveFileFromFolder()
        {
            Folder fold5 = _folder.GetFolderByID(5);
            var newFile = new File
            {
                ID = 2,
                Content = "var x = 0;",
                Name = "init.js",
                Extension = ".js",
                Type = "javascript",
                FolderID = 2
            };
            _folder.AddFileToFolder(5, newFile);
            //Act
            _folder.RemoveFileFromFolder(5, newFile);
            var result = _folder.GetAllFilesById(5);
            //Assert
            Assert.AreEqual(result.Count,0);
        }
    }
   
}
