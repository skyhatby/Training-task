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
            ValidatePath(path);

            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                throw new FileSystemServiceException(FileSystemError.DirectoryDoesntExists);
            }
            try
            {
                return dir.EnumerateDirectories();
            }
            catch (UnauthorizedAccessException)
            {
                throw new FileSystemServiceException(FileSystemError.YouHaventAccessToTheResource);
            }
        }

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public IEnumerable<FileInfo> GetAllFilesFromDirectory(string path)
        {
            ValidatePath(path);

            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                throw new FileSystemServiceException(FileSystemError.DirectoryDoesntExists);
            }
            try
            {
                return dir.EnumerateFiles();
            }
            catch (UnauthorizedAccessException)
            {
                throw new FileSystemServiceException(FileSystemError.YouHaventAccessToTheResource);
            }
        }

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public FileInfo GetFileByPath(string path)
        {
            ValidatePath(path);

            var file = new FileInfo(path);

            if (!file.Exists)
            {
                throw new FileSystemServiceException(FileSystemError.FileDoesntExists);
            }

            return file;
        }

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public DirectoryInfo GetDirectoryByPath(string path)
        {
            ValidatePath(path);

            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                throw new FileSystemServiceException(FileSystemError.DirectoryDoesntExists);
            }

            return dir;
        }

        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        public IEnumerable<DriveInfo> GetDrives()
        {
            var drives = DriveInfo.GetDrives();
            return drives;
        }

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public DirectoryInfo CreateDirectory(string path)
        {
            ValidatePath(path);

            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                try
                {
                    dir.Create();
                }
                catch (UnauthorizedAccessException)
                {
                    throw new FileSystemServiceException(FileSystemError.YouHaventAccessToTheResource);
                }
            }
            return dir;
        }

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public bool DeleteDirectory(string path)
        {
            ValidatePath(path);

            var dir = GetDirectoryByPath(path);

            try
            {
                dir.Delete();
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                throw new FileSystemServiceException(FileSystemError.YouHaventAccessToTheResource);
            }
        }

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        private bool ValidatePath(string path)
        {
            bool bOk = false;
            try
            {
                new System.IO.DirectoryInfo(path);
                bOk = true;
            }
            catch (ArgumentException) { }
            catch (PathTooLongException) { }
            catch (NotSupportedException) { }
            if (!bOk)
            {
                throw new FileSystemServiceException(FileSystemError.IncorrectPath);
            }
            else
            {
                return true;
            }
        }
    }
}
