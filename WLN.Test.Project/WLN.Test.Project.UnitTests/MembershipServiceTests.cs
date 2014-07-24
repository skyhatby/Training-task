using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WLN.Test.Project.Logic.Membership.Intarfaces;

namespace WLN.Test.Project.UnitTests
{
    [TestClass]
    public class MembershipServiceTests
    {
        private IMembershipService _membershipService;

        public MembershipServiceTests(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [TestMethod]
        public void GetRole()
        {
            var role = _membershipService.GetRoleByName("Administrator");
            Assert.IsNotNull(role);

            role.Dump();
        }
    }
}
