using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: PreApplicationStartMethod(typeof(WLN.Test.Project.Web.Modules.HttpModulesInitialiser), "Start")]
namespace WLN.Test.Project.Web.Modules
{
    public class HttpModulesInitialiser : IHttpModule
    {
        private Lazy<IEnumerable<IHttpModule>> _modules = new Lazy<IEnumerable<IHttpModule>>(RetrieveModules);

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(HttpModulesInitialiser));
        }

        private static IEnumerable<IHttpModule> RetrieveModules()
        {
            return DependencyResolver.Current.GetServices<IHttpModule>();
        }

        public void Dispose()
        {
            var modules = _modules.Value;

            foreach (var module in modules)
            {
                var disposableModule = module as IDisposable;

                if (disposableModule != null)
                {
                    disposableModule.Dispose();
                }
            }
        }

        public void Init(HttpApplication context)
        {
            var modules = _modules.Value;

            foreach (var module in modules)
            {
                module.Init(context);
            }
        }
    }
}