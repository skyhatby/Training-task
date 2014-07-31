using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLN.Test.Project.Logic.Membership
{
    public enum MembershipError
    {
        Ok,
        UserIsAlreadyRegistered,
        UserDoesNotExist,
        RoleDoesNotExist,
        NameOrPasswordMustBeNotNull,
        UnknownError
    }
}
