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
                throw new FileSystemServiceException(FileSystemError.DirectoryDoesntExist);
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
                throw new FileSystemServiceException(FileSystemError.DirectoryDoesntExist);
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
                throw new FileSystemServiceException(FileSystemError.DirectoryDoesntExist);
            }

            return dir;
        }

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public IEnumerable<DriveInfo> GetDrives()
        {
            try
            {
                var drives = DriveInfo.GetDrives();
                return drives;
            }
            catch (IOException)
            {
                throw new FileSystemServiceException(FileSystemError.UnknownError);
            }
            catch (UnauthorizedAccessException)
            {
                throw new FileSystemServiceException(FileSystemError.YouHaventAccessToTheResource);
            }
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
                dir.Delete(true);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                throw new FileSystemServiceException(FileSystemError.YouHaventAccessToTheResource);
            }
        }

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public FileInfo CreateFile(string path)
        {
            ValidatePath(path);
            var dirPath = Path.GetDirectoryName(path);
            CreateDirectory(dirPath);
            var file = new FileInfo(path);
            if (!file.Exists)
            {
                try
                {
                    var fileStream = file.Create();
                    fileStream.Close();
                }
                catch (UnauthorizedAccessException)
                {
                    throw new FileSystemServiceException(FileSystemError.YouHaventAccessToTheResource);
                }
            }
            return file;
        }

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        public bool DeleteFile(string path)
        {
            ValidatePath(path);

            var file = GetFileByPath(path);

            try
            {
                file.Delete();
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
