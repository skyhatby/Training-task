using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLN.Test.Project.Model.AuthModels
{
    class User : BaseClass
    {
        public virtual string Password { get; set; }
        public virtual string PasswordSalt { get; set; }
        public virtual Role Role { get; set; }
    }
}
