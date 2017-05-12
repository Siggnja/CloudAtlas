using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudAtlas.Models;

namespace CloudAtlas.Tests.Models
{
    [TestClass]
    public class GroupModelTest
    {
        [TestMethod]
        public void TestGroupName()
        {
            Group gr = new Group();
            gr.Name = "NewGroup";
            Assert.AreEqual(gr.Name, "NewGroup");
        }
        [TestMethod]
        public void TestGroupID()
        {
            Group gr = new Group();
            gr.ID = 1;
            Assert.AreEqual(gr.ID, 1);
        }
        [TestMethod]
        public void TestOwnerID()
        {
            Group gr = new Group();
            gr.OwnerID = "00537b30-1295-4e84-84b5-db7e76feee70";
            Assert.AreEqual(gr.OwnerID, "00537b30-1295-4e84-84b5-db7e76feee70");
        }
    }
}
