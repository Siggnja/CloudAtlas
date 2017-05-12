using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudAtlas.Repositories;
using CloudAtlas.Models;
using CloudAtlas.Tests.TestHelper;
using System.Collections.Generic;

namespace CloudAtlas.Tests.Repositories
{
    [TestClass]
    public class GroupRepositoryTest
    {
        private GroupsRepository _groups;
        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDatabase();
            InitMockData.InitData(mockDb);
            _groups = new GroupsRepository(mockDb);
        }
        [TestMethod]
        public void GetGroupByIdTest()
        {
            //Assert
            Assert.IsNotNull(_groups.GetGroupById(1));
        }
        [TestMethod]
        public void AddGroupTest()
        {
            //Arrange
            var group1 = new Group() { Name = "WolfPack", OwnerID = "00537b30-1295-4e84-84b5-db7e76feee70" };
            //Act
            _groups.AddGroup(group1);
            //Assert
            Assert.IsNotNull(_groups.GetGroupById(group1.ID));
        }
        [TestMethod]
        public void DeleteGroupById()
        {
            //Arrange
            var group1 = new Group() { Name = "Packed", OwnerID = "00537b30-1295-4e84-84b5-db7e76feee70" };
            //Act
            _groups.AddGroup(group1);
            _groups.DeleteGroupById(group1.ID);
            //Assert
            Assert.IsNull(_groups.GetGroupById(group1.ID));
        }
        [TestMethod]
        public void UserInGroupTest()
        {
            //Arrange
            ApplicationUser res = new ApplicationUser() { Email = "new@gmail.com", UserName = "New", Id = "00123c41-1295-4e84-84b5-db7e76feee70", EmailConfirmed = false, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 };
            var group1 = new Group() { Name = "Packed", OwnerID = "00123c41-1295-4e84-84b5-db7e76feee70", ApplicationUsers = new List<ApplicationUser>() };
           
            //Act
            group1.ApplicationUsers.Add(res);
            //Assert
            Assert.IsTrue(_groups.UserInGroup(res, group1));

        }
        [TestMethod]
        public void GetAllGroupsByUserId()
        {
            //Arrange
            var wolfpack = _groups.GetGroupById(1);
            //Act
            var pack = _groups.getAllGroupsByUserId("00537b30-1295-4e84-84b5-db7e76feee70");
            //Assert
            Assert.AreEqual(wolfpack, pack[0]);

        }

    }
}
