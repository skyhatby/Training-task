using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WLN.Test.Project.Logic.FileSystem.Interfaces;

namespace WLN.Test.Project.UnitTests
{
    [TestClass]
    public class FileSystemServiceTests : BaseTest
    {
        private IFileSystemService _fileSystemService;

        public FileSystemServiceTests()
        {
            _fileSystemService = DependencyResolver.Resolve<IFileSystemService>();
        }

        [TestMethod]
        public void GetDirectories()
        {
            var c = _fileSystemService.GetAllFoldersFromDirectory("C:\\");
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void GetFilesFromDirectory()
        {
            var c = _fileSystemService.GetAllFilesFromDirectory("C:\\");
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void GetFileByPath()
        {
            var c = _fileSystemService.GetFileByPath("C:\\Users\\desktop.ini");
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void GetDirectoryByPath()
        {
            var c = _fileSystemService.GetDirectoryByPath("C:\\Users");
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void GetDrives()
        {
            var c = _fileSystemService.GetDrives();
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void CreateDirectory()
        {
            var c = _fileSystemService.CreateDirectory("C:\\asdf\\asdf");
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void DeleteDirectory()
        {
            var c = _fileSystemService.CreateDirectory("C:\\asdf\\asdf");
            Assert.IsTrue(_fileSystemService.DeleteDirectory(c.Parent.FullName));
        }

        [TestMethod]
        public void CreateFile()
        {
            var c = _fileSystemService.CreateFile("C:\\asdf\\asdf.txt");
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void DeleteFile()
        {
            var c = _fileSystemService.CreateFile("C:\\asdf\\asdf.txt");
            Assert.IsTrue(_fileSystemService.DeleteFile(c.FullName));
        }
    }
}
