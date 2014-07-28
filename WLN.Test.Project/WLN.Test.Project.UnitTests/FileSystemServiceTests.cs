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
    }
}
