using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using WLN.Test.Project.Logic.Membership.Intarfaces;
using WLN.Test.Project.Logic.Membership;
using WLN.Test.Project.Model;
using WLN.Test.Project.DAL;
using NHibernate;
using WLN.Test.Project.Logic.FileSystem.Interfaces;
using WLN.Test.Project.Logic.FileSystem;
using System.Web;
using WLN.Test.Project.Web.Modules;

namespace WLN.Test.Project.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IHttpModule, MembershipModule>();
            container.RegisterType<IMembershipService, MembershipService>();
            container.RegisterType<IFileSystemService, FileSystemService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IRepositoryFactory, UnitOfWork>();
            container.RegisterInstance<ISessionFactory>(SessionFactoryManager.GetSessionFactory());
            container.RegisterType<ISession>(new InjectionFactory(c => c.Resolve<ISessionFactory>().OpenSession()));
        }
    }
}
