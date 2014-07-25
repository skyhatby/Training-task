using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLN.Test.Project.Model.Exceptions;

namespace WLN.Test.Project.Logic.Membership
{
    public class MembershipServiceException : ServiceException
    {
        public MembershipError Error { get; private set; }

        public MembershipServiceException(MembershipError error)
            : base("Check Error for details.")
        {
            Error = error;
        }
    }
}
