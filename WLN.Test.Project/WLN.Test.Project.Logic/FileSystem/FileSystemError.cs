﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLN.Test.Project.Logic.FileSystem
{
    public enum FileSystemError
    {
        Ok,
        FileDoesntExists,
        IncorrectPath,
        DirectoryDoesntExist,
        YouHaventAccessToTheResource,
        UnknownError
    }
}
