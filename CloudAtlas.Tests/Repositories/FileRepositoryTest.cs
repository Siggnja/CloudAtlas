using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudAtlas.Models;
using CloudAtlas.Repositories;
using CloudAtlas.Tests.TestHelper;

namespace CloudAtlas.Tests.Repositories
{    
    [TestClass]
    public class FileRepositoryTest
    {
        private FileRepository _file;
      
        [TestInitialize]
        public void Initialize()
        {
            var mockDb = new MockDatabase();
            InitMockData.InitData(mockDb);
            _file = new FileRepository(mockDb);
        }
        [TestMethod]
        public void TestGetFile()
        {
            //Arrange
            var newFile = _file.getFileById(1);
            //Assert
            Assert.IsNotNull(newFile);

        }
        [TestMethod]
        public void TestAddFile()
        {
            //Arrange
            var newFile = new File
            {
                ID = 6,
                Content = "var x = 'Hello';",
                Name = "init.js",
                Extension = ".js",
                Type = "javascript"
            };

            //Act
            _file.addFile(newFile);
            //Assert
            Assert.IsNotNull(_file.getFileById(newFile.ID));

        }
        [TestMethod]
        public void TestFileContent()
        {
            //Arrange
            var newFile = new File
            {
                ID = 6,
                Content = "var x = 'Hello';",
                Name = "init.js",
                Extension = ".js",
                Type = "javascript"
            };

            //Act
            _file.addFile(newFile);
            //Assert
            Assert.IsTrue(newFile.Content.Equals("var x = 'Hello';"));

        }

        [TestMethod]
        public void TestRemoveFile()
        {
            //Arrange
            var newFile = new File
            {
                ID = 6,
                Content = "var x = 'Hello';",
                Name = "init.js",
                Extension = ".js",
                Type = "javascript"
            };
            //Act
            _file.addFile(newFile);
            _file.deleteFile(newFile);
            //Assert
            Assert.IsNull(_file.getFileById(6));
        }
    }
}
