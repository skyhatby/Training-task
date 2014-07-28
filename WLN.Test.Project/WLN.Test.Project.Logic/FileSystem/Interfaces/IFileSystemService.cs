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
        IEnumerable<DirectoryInfo> GetAllFoldersFromDirectory(string path);
        IEnumerable<FileInfo> GetAllFilesFromDirectory(string path);
        FileInfo GetFileByPath(string path);
    }
}
