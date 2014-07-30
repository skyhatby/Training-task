using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLN.Test.Project.Logic.Common;

namespace WLN.Test.Project.Logic.FileSystem.Interfaces
{
    public interface IFileSystemService : IService
    {
        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        IEnumerable<DirectoryInfo> GetAllFoldersFromDirectory(string path);

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        IEnumerable<FileInfo> GetAllFilesFromDirectory(string path);

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        FileInfo GetFileByPath(string path);

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        DirectoryInfo GetDirectoryByPath(string path);

        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        IEnumerable<DriveInfo> GetDrives();

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        DirectoryInfo CreateDirectory(string path);

        /// <exception cref="WLN.Test.Project.Logic.FileSystem.FileSystemServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        bool DeleteDirectory(string path);
    }
}
