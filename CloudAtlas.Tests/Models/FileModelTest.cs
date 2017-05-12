using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudAtlas.Models;

namespace CloudAtlas.Tests.Models
{
    /// <summary>
    /// Summary description for FileModelTest
    /// </summary>
    [TestClass]
    public class FileModelTest
    {
      
        [TestMethod]
        public void TestFileContent()
        {
            File res = new File();
            res.Content = "<html></html>";
            Assert.AreEqual(res.Content, "<html></html>");
            //
            // TODO: Add test logic here
            //
        }
        [TestMethod]
        public void TestFileName()
        {
            File res = new File();
            res.Name = "init.js";
            Assert.AreEqual(res.Name, "init.js");
            //
            // TODO: Add test logic here
            //
        }
        [TestMethod]
        public void TestFileType()
        {
            File res = new File();
            res.Type = "javascript";
            Assert.AreEqual(res.Type, "javascript");
            //
            // TODO: Add test logic here
            //
        }
        [TestMethod]
        public void TestFileExentsion()
        {
            File res = new File();
            res.Extension = ".js";
            Assert.AreEqual(res.Extension, ".js");
            //
            // TODO: Add test logic here
            //
        }
        [TestMethod]
        public void TestFileId()
        {
            File res = new File();
            res.ID = 1;
            Assert.AreEqual(res.ID, 1);
            //
            // TODO: Add test logic here
            //
        }
        [TestMethod]
        public void TestFolderId()
        {
            File res = new File();
            res.FolderID = 1;
            Assert.AreEqual(res.FolderID, 1);
            //
            // TODO: Add test logic here
            //
        }
    }
}
