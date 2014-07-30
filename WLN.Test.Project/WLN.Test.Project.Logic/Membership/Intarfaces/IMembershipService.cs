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
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        Role GetRoleByName(string roleName);

        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        User GetUser(long userId);

        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        User GetUserByName(string userName);

        /// <exception cref="WLN.Test.Project.Logic.Membership.MembershipServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        User RegisterUser(string name, string password, string roleName = "User");

        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        void UpdateUser(User user);

        /// <exception cref="WLN.Test.Project.Logic.Membership.MembershipServiceException"></exception>
        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        void ResetPassword(string name);

        /// <exception cref="WLN.Test.Project.Logic.Common.ServiceException"></exception>
        string[] GetUserRoles(long userId);
    }
}
