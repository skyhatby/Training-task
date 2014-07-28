using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLN.Test.Project.Logic.FileSystem.Interfaces;

namespace WLN.Test.Project.Logic.FileSystem
{
    public class FileSystemService : IFileSystemService
    {
        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public IEnumerable<DirectoryInfo> GetAllFoldersFromDirectory(string path)
        {
            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                throw new FileSystemServiceException(FileSystemError.DirectoryDoesntExists);
            }

            return dir.EnumerateDirectories();
        }

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public IEnumerable<FileInfo> GetAllFilesFromDirectory(string path)
        {
            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                throw new FileSystemServiceException(FileSystemError.DirectoryDoesntExists);
            }

            return dir.EnumerateFiles();
        }

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public FileInfo GetFileByPath(string path)
        {
            var file = new FileInfo(path);

            if (!file.Exists)
            {
                throw new FileSystemServiceException(FileSystemError.FileDoesntExists);
            }

            return file;
        }
    }
}
