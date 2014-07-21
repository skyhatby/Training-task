using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLN.Test.Project.Helpers;

namespace WLN.Test.Project.Model.AuthModels
{
    public class User : NamedEntity
    {
        private string _password;

        #region ctors

        public User()
        {
            PasswordSault = UsersHelper.GeneratePassword();
        }

        public User(string name, string pass)
            : this()
        {
            Name = name;
            Password = pass;
        }

        public User(string name, string pass, Role role)
            : this(name, pass)
        {
            Role = role;
        } 

        #endregion

        public virtual string PasswordSault { get; private set; }
        public virtual string Password
        {
            get { return _password; }
            set { _password = UsersHelper.GetHash(value, PasswordSault); }
        }

        public virtual Role Role { get; set; }
    }
}
