using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudAtlas.Tests.TestHelper;
using CloudAtlas.Models;
using CloudAtlas.Repositories;

namespace CloudAtlas.Tests.Repositories
{
    [TestClass]
    public class ProjectRepositoryTest
    {
        ProjectsRepository _Project;
        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDatabase();
            InitMockData.InitData(mockDb);
            _Project = new ProjectsRepository(mockDb);
        }
        [TestMethod]
        public void GetProjectByIdTest()
        {
            //Arrange
            var proj1 = _Project.GetProjectById(1);

            //Assert
            Assert.IsNotNull(proj1);
        }
        [TestMethod]
        public void AddProjectTest()
        {
            //Arrange
            var newProject = new Project() {ID=10, Name = "Project10", Type = "Javascript", IsGroupProject = false };

            //Act
            _Project.AddProject(newProject);
            //Assert
            Assert.IsNotNull(_Project.GetProjectById(newProject.ID));
        }
        [TestMethod]
        public void RemoveProjectTest()
        {
            var proj = _Project.GetProjectById(1);
            //Act
            _Project.RemoveProject(proj);
            //Assert
            Assert.IsNull(_Project.GetProjectById(proj.ID));
        }
        [TestMethod]
        public void GetProjectByUserIdTest()
        {
            //Arrange
            var pro1 = _Project.GetProjectById(1);
            var pro2 = _Project.GetProjectById(2);
            //Act
            var res = _Project.GetProjectsByUserId("00537b30-1295-4e84-84b5-db7e76feee70");
            //Assert
            Assert.AreEqual(pro1, res[0]);
            Assert.AreEqual(pro2, res[1]);
;       }
        [TestMethod]
        public void GetUserByIdTest()
        {
            //Arrange
            Assert.IsNotNull(_Project.GetUserByID("00537b30-1295-4e84-84b5-db7e76feee70"));
        }
        [TestMethod]
        public void GetUserByEmail()
        {
            //Arrange
            Assert.IsNotNull(_Project.GetUserByEmail("siggnja@gmail.com"));
        }
        [TestMethod]
        public void SearchUserByEmailTest()
        {
            Assert.IsNotNull(_Project.SearchUserByEmail("siggnja", "f380908a-8f2e-4441-ae2f-b2a07f8b0e36"));
        }

    }
}
