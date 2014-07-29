using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLN.Test.Project.Logic.Membership.Identity
{
    public class UserInfo
    {
        public long UserId { get; set; }

        public override string ToString()
        {
            return UserId.ToString();
        }

        public static UserInfo FromString(string s)
        {
            return new UserInfo { UserId = Int64.Parse(s) };
        }
    }
}
