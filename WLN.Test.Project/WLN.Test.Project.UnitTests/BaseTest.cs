using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using WLN.Test.Project.Web.App_Start;

namespace WLN.Test.Project.UnitTests
{
    [TestClass]
    public class BaseTest
    {
        public static IUnityContainer DependencyResolver { get; private set; }

        [AssemblyInitialize]
        public static void BeforeAnyTestInProject(TestContext context)
        {
            DependencyResolver = UnityConfig.GetConfiguredContainer();
        }
    }
}
