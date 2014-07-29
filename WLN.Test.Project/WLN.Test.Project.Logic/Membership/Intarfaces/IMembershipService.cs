using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLN.Test.Project.Logic.Common;
using WLN.Test.Project.Model.MemberhshipModels;

namespace WLN.Test.Project.Logic.Membership.Intarfaces
{
    public interface IMembershipService : IService
    {
        Role GetRoleByName(string roleName);
        User GetUser(long userId);
        User GetUserByName(string userName);
        User RegisterUser(string name, string password, string roleName);
        void UpdateUser(User user);
        void ResetPassword(string name);
        string[] GetUserRoles(long userId);
    }
}
