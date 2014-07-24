using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WLN.Test.Project.Logic.Membership.Intarfaces;

namespace WLN.Test.Project.UnitTests
{
    [TestClass]
    public class MembershipServiceTests : BaseTest
    {
        private IMembershipService _membershipService;

        public MembershipServiceTests()
        {
            _membershipService = DependencyResolver.Resolve<IMembershipService>();
        }

        [TestMethod]
        public void GetRole()
        {
            var role = _membershipService.GetRoleByName("Administrator");
            Assert.IsNotNull(role);
        }
    }
}
