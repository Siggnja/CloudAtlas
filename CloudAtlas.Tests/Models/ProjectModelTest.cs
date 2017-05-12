using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudAtlas.Models;

namespace CloudAtlas.Tests.Models
{
    [TestClass]
    public class ProjectModelTest
    {
        [TestMethod]
        public void TestProjectId()
        {
            Project proj = new Project();
            proj.ID = 1;
            Assert.AreEqual(proj.ID, 1);

        }
        [TestMethod]
        public void TestProjectName()
        {
            Project proj = new Project();
            proj.Name = "Project1";
            Assert.AreEqual(proj.Name, "Project1");

        }
        [TestMethod]
        public void TestProjectType()
        {
            Project proj = new Project();
            proj.Type = "javascript";
            Assert.AreEqual(proj.Type, "javascript");

        }
        [TestMethod]
        public void TestProjectFolderID()
        {
            Project proj = new Project();
            proj.FolderID = 1;
            Assert.AreEqual(proj.FolderID, 1);
        }
        [TestMethod]
        public void TestProjectDateCreated()
        {
            Project proj = new Project();
            proj.DateCreated = DateTime.Now;
            Assert.IsNotNull(DateTime.Now);
        }
        [TestMethod]
        public void TestProjectDateOwnerID()
        {
            Project proj = new Project();
            proj.OwnerID = "00537b30-1295-4e84-84b5-db7e76feee70";
            Assert.AreEqual(proj.OwnerID, "00537b30-1295-4e84-84b5-db7e76feee70");
        }

    }
}
