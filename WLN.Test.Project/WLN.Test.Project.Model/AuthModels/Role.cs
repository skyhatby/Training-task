using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLN.Test.Project.Model.AuthModels
{
    public class Role : NamedEntity
    {
        private IEnumerable<User> _users;

        #region ctors

        public Role()
        {
            _users = new List<User>();
        }

        public Role(string name)
            : this()
        {
            Name = name;
        } 

        #endregion

        public virtual IEnumerable<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }
    }
}
