using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using WLN.Test.Project.Model;
using System.Linq.Expressions;
using WLN.Test.Project.Model.MemberhshipModels;

namespace WLN.Test.Project.UnitTests
{
    [TestClass]
    public class RepoTests : BaseTest
    {
        private IRepositoryFactory _repositoryFactory;

        public RepoTests()
        {
            _repositoryFactory = DependencyResolver.Resolve<IRepositoryFactory>();
        }

        [TestMethod]
        public void RepoFind()
        {
            var c = _repositoryFactory.UserRepository;
            Assert.IsNotNull(c.Find(1));
        }

        [TestMethod]
        public void RepoFindWithPredicate()
        {
            var c = _repositoryFactory.UserRepository;
            Expression<Func<User, bool>> expr = x => x.Name == "skyhat";
            Assert.IsNotNull(c.Find(expr));
        }

        [TestMethod]
        public void RepoAll()
        {
            var c = _repositoryFactory.UserRepository;
            Assert.IsNotNull(c.All());
        }

        [TestMethod]
        public void RepoFilter()
        {
            var c = _repositoryFactory.UserRepository;
            Expression<Func<User, bool>> expr = x => x.IsInRole("User");
            Assert.IsNotNull(c.Filter(expr));
        }

        [TestMethod]
        public void RepoCreate()
        {
            var c = _repositoryFactory.UserRepository;
            var user = new User() { Name = "asdf" };
            user.SetPassword("password");
            c.Create(user);
            Expression<Func<User, bool>> expr = x => x.Name == "asdf";
            c.Find(expr);
            Assert.IsNotNull(c.Filter(expr));
        }

        [TestMethod]
        public void RepoUpdate()
        {
            var c = _repositoryFactory.UserRepository;
            Expression<Func<User, bool>> expr = x => x.Name == "asdf";
            var user = c.Find(expr);
            var pass1 = user.Password;
            user.SetPassword("111111");
            c.Update(user);
            Assert.AreNotEqual(pass1,c.Find(expr).Password);
        }

        [TestMethod]
        public void RepoDelete()
        {
            var c = _repositoryFactory.UserRepository;
            Expression<Func<User, bool>> expr = x => x.Name == "asdf";
            var user = c.Find(expr);
            c.Delete(user);
            Assert.IsNull(c.Find(expr));
        }
    }
}
