using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WLN.Test.Project.Logic.Membership.Identity
{
    public class UserIdIdentity : IIdentity
    {
        public string Name { get; set; }
        public long UserId { get; set; }
        public string AuthenticationType { get { return "UserIdIdentity"; } }
        public bool IsAuthenticated { get; set; }
    }
}
