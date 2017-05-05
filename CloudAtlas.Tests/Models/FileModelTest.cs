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
        public FileModelTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        [TestMethod]
        public void TestContent()
        {
            File res = new File();
            res.Content = "<html></html>";
            Assert.AreEqual(res.Content, "<html></html>");
            //
            // TODO: Add test logic here
            //
        }
        [TestMethod]
        public void TestName()
        {
            File res = new File();
            res.Name = "init.js";
            Assert.AreEqual(res.Name, "init.js");
            //
            // TODO: Add test logic here
            //
        }
    }
}
