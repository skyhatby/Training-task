using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLN.Test.Project.Helpers;

namespace WLN.Test.Project.Model.AuthModels
{
    public class User : EntityWhithName<long>
    {
        private IEnumerable<Role> _roles;

        public virtual string Password { get; set; }
        public virtual string PasswordSalt { get; set; }

        public virtual IEnumerable<Role> Roles
        {
            get { return _roles ?? (_roles = new HashSet<Role>()); }
            set { _roles = value; }
        }

        public virtual bool VerifyPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && Password == UsersHelper.GetHash(password, PasswordSalt);
        }

        public virtual void SetPassword(string password)
        {
            PasswordSalt = UsersHelper.GeneratePassword();
            Password = UsersHelper.GetHash(password, PasswordSalt);
        }

        /// <exception cref="System.ArgumentNullException"></exception>
        public virtual bool IsInRole(string roleName)
        {
            Expect.ArgumentNotNull(roleName, "roleName");

            return Roles.Any(x => x.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
        }

        public virtual bool IsInRole(int roleId)
        {
            return Roles.Any(x => x.Id == roleId);
        }
    }
}
