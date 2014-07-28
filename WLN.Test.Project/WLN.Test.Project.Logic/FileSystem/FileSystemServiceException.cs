using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLN.Test.Project.Model.Exceptions;

namespace WLN.Test.Project.Logic.FileSystem
{
    class FileSystemServiceException : ServiceException
    {
        public FileSystemError Error { get; private set; }

        public FileSystemServiceException(FileSystemError error)
            : base("Check Error for details.")
        {
            Error = error;
        }
    }
}
