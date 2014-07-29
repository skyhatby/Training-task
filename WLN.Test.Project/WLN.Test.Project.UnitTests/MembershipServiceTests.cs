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

        [TestMethod]
        public void RegisterUser()
        {
            var name = Guid.NewGuid().ToString();
            var user = _membershipService.RegisterUser(name, "password", "User");
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetUserByName()
        {
            var name = "skyhat";
            var user = _membershipService.GetUserByName(name);
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void ResetUserPassword()
        {
            var name = "skyhat";
            var pass1 = _membershipService.GetUserByName(name).Password;
            _membershipService.ResetPassword(name);
            var pass2 = _membershipService.GetUserByName(name).Password;
            Assert.AreNotEqual(pass1, pass2);
        }

        [TestMethod]
        public void CheckIfUserRolesHasAdminRole()
        {
            var role = _membershipService.GetUserRoles(1);
            var b = role[0];
            Assert.AreEqual(b, "Administrator");
        }
    }
}
