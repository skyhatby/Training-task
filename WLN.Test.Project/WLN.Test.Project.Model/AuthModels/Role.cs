using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLN.Test.Project.Model.AuthModels
{
    public class Role : EntityWhithName
    {
        public const int Undefined = 0;
        public const int Administrator = 1;
        public const int User = 2;

        #region ctors

        public Role()
        {
        }

        public Role(string name)
            : this()
        {
            Name = name;
        } 

        #endregion

        
    }
}
