using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudAtlas.Models;

namespace CloudAtlas.Tests
{
    /// <summary>
    /// Summary description for FolderModelTest
    /// </summary>
    [TestClass]
    public class FolderModelTest
    {
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestFolderName()
        {
            Folder test = new Folder();
            test.Name = "Root";
            Assert.AreEqual(test.Name.ToString(), "Root");
        }
        [TestMethod]
        public void TestFolderIsRoot()
        {
            Folder test = new Folder();
            test.IsRoot = false;
            Assert.AreEqual(test.IsRoot, false);
        }
        [TestMethod]
        public void TestFiles()
        {
            Folder test = new Folder();
            test.Files = new List<File>();
            test.Files.Add(new File { ID = 1, Name = "Temp" });
            Assert.AreEqual(test.Files.Count, 1);
        }



    }
}
